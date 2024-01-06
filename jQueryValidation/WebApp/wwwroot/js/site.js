function focusOnValidationErrorInput(valErrors) {
    // valErrorsが存在し、最初の要素が存在するか確認
    if (valErrors && valErrors.length > 0) {
        // 一つ目の memberNames を取り出す
        var firstMemberName = valErrors[0].memberNames[0];

        // その値の id を持つ input 要素を取得してフォーカス
        var targetInput = document.getElementById(firstMemberName);
        if (targetInput) {
            targetInput.focus();
        }
    }
}

/**
 * jquery.validate.unobtrusive.jsを参考
 * @param {string} value - selectorの文字を適切にエスケープ（jQuery仕様）
 * @returns {string} - エスケープ済み
 */
function escapeAttributeValue(value) {
    // As mentioned on http://api.jquery.com/category/selectors/
    return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
}

function clearValidationErrors($root) {
    // $root 要素の子孫で 'data-valmsg-for' 属性を持つ要素を取得
    var $containers = $root.find('[data-valmsg-for]');
    $containers.empty();　// 各要素の中身を空にする

    //showValidationErrorでinput-validation-errorが付与されるので削除する。
    var nameArray = $containers.map(function () {
        return $(this).attr('data-valmsg-for');
    }).get();
    nameArray.forEach(function (name) {
        const $inputElem = $root.find('[name="' + name + '"]');
        $inputElem.removeClass("input-validation-error");
    });
}

function clearValidationErrorsVanilla(rootElem) {
    // rootElem 要素の子孫で 'data-valmsg-for' 属性を持つ要素を取得
    var containers = rootElem.querySelectorAll('[data-valmsg-for]');

    // 各要素の中身を空にする
    containers.forEach(function (container) {
        container.innerHTML = '';
    });

    // input-validation-error クラスを持つ要素からクラスを削除する
    containers.forEach(function (container) {
        var name = container.getAttribute('data-valmsg-for');
        var inputElem = rootElem.querySelector('[name="' + name + '"]');

        if (inputElem) {
            inputElem.classList.remove("input-validation-error");
        }
    });
}


function showValidationErrors($root, validationErrors) {
    validationErrors.forEach(function (valError) {
        const elemNames = valError.memberNames;
        const errorMessage = valError.errorMessage;

        elemNames.forEach(function (name) {
            const $inputElem = $root.find('[name="' + name + '"]');
            showValidationError($root, $inputElem, errorMessage);
        });
    });
}

function showValidationErrorsVanilla(rootElem, validationErrors) {
    validationErrors.forEach(function (valError) {
        var elemNames = valError.memberNames;
        var errorMessage = valError.errorMessage;

        elemNames.forEach(function (name) {
            var inputElem = rootElem.querySelector('[name="' + name + '"]');
            if (inputElem) {
                showValidationErrorVanilla(rootElem, inputElem, errorMessage);
            }
        });
    });
}

function showValidationError($root, $input, errorMessage) {  // 'this' is the form element
    const $error = $('<span>', {
        id: `${$input[0].name}-error`,
        class: '',
        text: errorMessage
    });
    //jQueryValidateではinput-validation-errorが付与されるのであわせる。
    $input.removeClass("input-validation-error").addClass("input-validation-error");
    const container = $root.find("[data-valmsg-for='" + escapeAttributeValue($input[0].name) + "']");
    container.removeClass("field-validation-valid").addClass("field-validation-error");
    container.empty();
    $error.removeClass("input-validation-error").appendTo(container);
}

function showValidationErrorVanilla(rootElem, inputElem, errorMessage) {
    // エラーメッセージの表示用要素を作成
    var errorSpan = document.createElement('span');
    errorSpan.id = inputElem.name + '-error';
    errorSpan.textContent = errorMessage;

    // inputElem にクラスを追加
    inputElem.classList.remove("input-validation-error");
    inputElem.classList.add("input-validation-error");

    // エラーコンテナの取得
    var container = rootElem.querySelector('[data-valmsg-for="' + escapeAttributeValue(inputElem.name) + '"]');

    // クラスの追加・削除とエラーメッセージの設定
    container.classList.remove("field-validation-valid");
    container.classList.add("field-validation-error");
    container.innerHTML = '';
    container.appendChild(errorSpan);
}

/**
 * フォームデータを非同期で送信する関数
 * @param {Event} e - イベントオブジェクト
 * @param {HTMLElement} formElem - フォーム要素（action属性,method属性が必須）
 * @param {string} antiXsrfToken - CSRFトークン
 * @returns {Promise} - フェッチリクエストのPromiseオブジェクト
 */
function submitFormDataAsync(e, formElem, antiXsrfToken) {
    return new Promise((resolve, reject) => {
        e.preventDefault(); // form処理停止

        const url = formElem.getAttribute("action");
        const method = formElem.getAttribute("method");
        const data = new FormData(formElem);

        fetch(url, {
            method: method,
            body: data,
            headers: {
                'RequestVerificationToken': antiXsrfToken
            }
        }).then(response => {
            if (!response.ok) {
                reject(new Error('Network response was not ok'));
            } else {
                resolve(response);
            }
        }).catch(error => {
            reject(error);
        });
    });
}

function fetchPostAsync(url, jsonObj, antiXsrfToken) {
    return new Promise((resolve, reject) => {
        fetch(url, {
            method: 'POST',
            body: JSON.stringify(jsonObj),
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': antiXsrfToken
            }
        }).then(response => {
            if (!response.ok) {
                reject(new Error('Network response was not ok'));
            } else {
                resolve(response.json());
            }
        }).catch(error => {
            reject(error);
        });
    });
}

/**
* jsonを非同期で送信する関数。HtmlDOMを応答するAPI用。
* 但し検証エラーの際は202でJSONを応答する。
* @param {string} url - POSTするURL
* @param {HTMLElement} jsonObj - 送信するJSONオブジェクト
* @param {string} antiXsrfToken - CSRFトークン
* @returns {Promise} - フェッチリクエストのPromiseオブジェクト
*/
function fetchHtmlDOMPostAsync(url, jsonObj, antiXsrfToken) {
    return new Promise((resolve, reject) => {
        fetch(url, {
            method: 'POST',
            body: JSON.stringify(jsonObj),
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': antiXsrfToken
            }
        }).then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            } else {
                if (response.status === 200) {
                    response.text().then(htmlText => {
                        const parser = new DOMParser();
                        const htmlDOM = parser.parseFromString(htmlText, 'text/html'); //<html><body>が付与されるので除外
                        const targetDOM = htmlDOM.querySelector('body').children;
                        resolve({ status: response.status, data: targetDOM });
                    });
                } else if (response.status === 202) {
                    response.json().then(jsonObj => {
                        resolve({ status: response.status, data: jsonObj });
                    });
                } else {
                    throw new Error('Network response was not ok');
                }
            }
        }).catch(error => {
            reject(error);
        });
    });
}
