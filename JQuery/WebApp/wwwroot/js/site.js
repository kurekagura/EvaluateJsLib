(function ($) {
    $.fn.mySequenceTable = function (options) {
        const self = this; //JqObj

        // 初期化
        if (typeof options === 'object') {
            self.find('[data-my-up]').on('click', (e) => {
                console.log('up clicked');
                const $table = self.find('table');
                const selRows = $table.bootstrapTable('getSelections');
                if (selRows.length < 1)
                    return;

                const data = $table.bootstrapTable('getData');
                let beforeIndex = 0; //現在0もNGの為
                $.each(data, function (i, item) {
                    if (item.id === selRows[0].id) {
                        beforeIndex = i;
                        return false;  // eachループを終了
                    }
                });
                if (beforeIndex === 0)
                    return;

                const afterIndex = beforeIndex - 1;
                const swapData = swapRows(data, beforeIndex, afterIndex);
                $table.bootstrapTable('load', swapData);
                self.trigger('sequenceChanged.mysequences', [beforeIndex, afterIndex]);
            });
            self.find('[data-my-down]').on('click', (e) => {
                console.log('down clicked');
                const $table = self.find('table');
                const selRows = $table.bootstrapTable('getSelections');
                if (selRows.length < 1)
                    return;

                const data = $table.bootstrapTable('getData');
                const lastIndex = data.length - 1;
                let beforeIndex = lastIndex; //lastIndexもNGの為
                $.each(data, function (i, item) {
                    if (item.id === selRows[0].id) {
                        beforeIndex = i;
                        return false;  // eachループを終了
                    }
                });
                if (beforeIndex === lastIndex)
                    return;

                const afterIndex = beforeIndex + 1;
                const swapData = swapRows(data, beforeIndex, afterIndex);
                $table.bootstrapTable('load', swapData);
                self.trigger('sequenceChanged.mysequences', [beforeIndex, afterIndex]);
            });
        }

        // メソッドの呼び出し
        if (typeof options === 'string') {
            var args = Array.prototype.slice.call(arguments, 1);
            const $table = self.find('table');
            switch (options) {
                case 'appendSequence':
                    const dispText = args[0];
                    /*const callback = args[1];*/
                    const newIndex = appendSequenceInternal($table, dispText);
                    //console.log('[callee]追加index', newIndex);
                    //callback.call(self, newIndex);　//呼び出し元をjQueryPluginにする
                    return this;
                case 'removeSequence':
                    const removingIndex = args[0];
                    $table.bootstrapTable('remove', { field: '$index', values: [removingIndex] });
                    //idを詰める
                    const data = $table.bootstrapTable('getData');
                    for (var i = data.length - 1; i >= 0; i--) {
                        if (i < removingIndex) {
                            break;
                        }
                        data[i].id = data[i].id - 1;
                    }
                    $table.bootstrapTable('load', data);
                    return this;
            }
        }

        function appendSequenceInternal($table, dispText) {
            const data = $table.bootstrapTable('getData');
            const newIndex = data.length;
            $table.bootstrapTable('append', [{ id: newIndex, radio: false, name: dispText }]);
            return newIndex;

        };

        function swapRows(jsonData, before, after) {
            // 行のスワップ
            const tmp = jsonData[before];
            jsonData[before] = jsonData[after];
            jsonData[after] = tmp;

            const tmpid = jsonData[before].id;
            jsonData[before].id = jsonData[after].id;
            jsonData[after].id = tmpid;
            return jsonData;
        }

        return this;
    };
})(jQuery);

function swapElement(parentSelector, beforeIndex, afterIndex) {
    var gIndex, sIndex;

    if (beforeIndex > afterIndex) {
        gIndex = beforeIndex;
        sIndex = afterIndex;
    } else {
        gIndex = afterIndex;
        sIndex = beforeIndex;
    }

    const $parent = $(parentSelector);
    const $children = $parent.children();

    $children.eq(sIndex).insertAfter($children.eq(gIndex));
    $children.eq(gIndex).insertBefore($children.eq(sIndex));
}

(function ($) {
    $.fn.myMenu = function () {
        const self = this;

        //var selectedValue = self.find('button.dropdown-item').attr('data-value');
        self.on('click', 'button.dropdown-item', (evt) => {
            var clickedValue = $(evt.target).attr('data-value');
            var clickedText = $(evt.target).attr('data-text');
            self.trigger('selected.mymenu', [clickedValue, clickedText]);
        });
        return self;
    };
})(jQuery);

(function ($) {
    $.fn.mySequences = function (options) {
        const self = this; //JqObj

        // 初期化
        if (typeof options === 'object') {
            self.find('[data-my-up]').on('click', (e) => {
                console.log('up clicked');
                const $parent = self.find('.list-group');
                const $activeItem = $parent.children('.list-group-item.active');
                if ($activeItem.length > 0) {
                    const before = $activeItem.index(); //移動前index
                    var $prevItem = $activeItem.prev();
                    if ($prevItem.length > 0) {
                        // 'active' クラスが付いている要素と一つ上の要素を入れ替え
                        $prevItem.before($activeItem);
                        const after = $activeItem.index(); //移動後index
                        self.trigger('sequenceChanged.mysequences', [before, after]);
                    }
                }
            });
            self.find('[data-my-down]').on('click', (e) => {
                console.log('down clicked');
                const $parent = self.find('.list-group');
                const $activeItem = $parent.children('.list-group-item.active');
                if ($activeItem.length > 0) {
                    const before = $activeItem.index(); //移動前index
                    var $nextItem = $activeItem.next();
                    if ($nextItem.length > 0) {
                        $nextItem.after($activeItem);
                        const after = $activeItem.index(); //移動後index
                        self.trigger('sequenceChanged.mysequences', [before, after]);
                    }
                }
            });
        }

        // メソッドの呼び出し
        if (typeof options === 'string') {
            var args = Array.prototype.slice.call(arguments, 1);
            const $parent = self.find('.list-group');
            switch (options) {
                case 'appendSequence':
                    const dispText = args[0];
                    const callback = args[1];
                    const newIndex = appendSequenceInternal(dispText);
                    console.log('[callee]追加index', newIndex);
                    callback.call(self, newIndex);　//呼び出し元をjQueryPluginにする
                    return this;
                case 'removeSequence':
                    const removingIdx = args[0];
                    const $child = $parent.children().eq(removingIdx);
                    $child.off('click'); //appendSequenceでonしている。
                    $child.remove();
                    return this;
                case 'selectIndex':
                    console.log('[callee]selectIndex');
                    const index = args[0];
                    // index番目の子要素に対してclickイベントを発生させる
                    $parent.children().eq(index).trigger('click');
                    return this;
            }
        }

        function appendSequenceInternal(dispText) {
            var $newButton = $('<button type="button" class="list-group-item list-group-item-action"></button>')
                .text(dispText)
                .on('click', (e) => {
                    var $clickedBtn = $(e.target);
                    var isActive = $clickedBtn.hasClass('active'); // 'active' クラスが付いているかどうかを判定

                    // クリックされたリストアイテムに active クラスを付与し、他の兄弟要素からは active クラスを削除
                    $clickedBtn.toggleClass('active').siblings().removeClass('active');
                    // クリックされたリストアイテムが 'active' クラスを削除された場合、index を undefined に設定
                    var index = isActive ? undefined : $clickedBtn.index();

                    self.trigger('selectionChanged.mysequences', [index]);
                });
            const $parent = self.find('.list-group');
            $parent.append($newButton);
            var newIndex = $parent.children().index($newButton);
            return newIndex;
        };

        return this;
    };
})(jQuery);

(function ($) {
    $.fn.myPluginA = function (options) {
        var defaults = {
            apiUrl: '',
        };
        // オプションのマージ
        var settings = $.extend({}, defaults, options);

        // 初期化
        if (typeof options === 'object') {
            // ここに初期化のコードを追加
            // settings.apiUrl を使用して必要な初期化を行う
        }

        // メソッドの呼び出し
        if (typeof options === 'string') {
            var args = Array.prototype.slice.call(arguments, 1);

            switch (options) {
                case 'setImage':
                    //return this.each(function () {
                    //    var $img = $(this).find('img');
                    //    $img.attr('src', args[0]);
                    //});
                    setImageInternal(this);
                    return this;
                case 'getFormData':
                    var callback = args[0];
                    var formData = getFormDataInternal(this);
                    callback(formData);
                    return this;
            }
        }

        function setImageInternal(pluginJqObj) {
            console.log('setImageInternalが呼び出された', pluginJqObj);
        }

        function getFormDataInternal(pluginJqObj) {
            console.log('getFormDataInternalが呼び出された', pluginJqObj);
            return {};
        }

        // 例: イベントリスナーの設定など
        this.on('submit', 'form', (e) => {
            e.preventDefault();
            var $form = $(e.target);
            var fd = new FormData($form[0]);
            console.log('submitをフック。fd抽出', fd.get('name1'));
        });

        return this; // チェーン可能にするために this を返す
    };
})(jQuery);
