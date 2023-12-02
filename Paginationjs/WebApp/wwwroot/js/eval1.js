function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

$(function () {
    $('#idpager').pagination({
        dataSource: async function (createDummyDataFunc) {
            var result = [];
            for (var i = 1; i <= 100; i++) {
                const now = new Date();
                const h = now.getHours();
                const min = now.getMinutes();
                const sec = now.getSeconds();
                const millsec = now.getTime();
                const row = { title: 'data_' + i, date: h + ':' + min + ':' + sec + '.' + millsec };
                result.push(row); //配列に追加
                await sleep(10);
            }
            createDummyDataFunc(result);
        },
        showPageNumbers: false,
        showNavigator: true,
        showGoInput: true,
        showGoButton: true,
        pageSize: 2,
        //pageRange: 3, //選択ページボタンの両側に表示する個数
        prevText: '&lt;',
        nextText: '&gt;',
        callback: function (data, pagination) {
            console.log('Changing page');
            // ページめくりで呼ばれる。dataをHTML要素に変換。
            $('#idcontents').html(createElement(data));
        }
    });
});

function createElement(rowsPerPage) {
    return rowsPerPage.map(function (row) {
        return '<li class="list">'
            + '<p class="title">' + row.title + '</p>'
            + '<p class="date">' + row.date + '</p></li>'
    })
}