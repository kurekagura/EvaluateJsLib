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

function showValidationError($root, $inputElement, errorMessage) {  // 'this' is the form element
    const $error = $('<span>', {
        id: `${$inputElement[0].name}-error`,
        class: '',
        text: errorMessage
    });
    //jQueryValidateではinput-validation-errorが付与されるのであわせる。
    $inputElement.removeClass("input-validation-error").addClass("input-validation-error");
    const container = $root.find("[data-valmsg-for='" + escapeAttributeValue($inputElement[0].name) + "']");
    container.removeClass("field-validation-valid").addClass("field-validation-error");
    container.empty();
    $error.removeClass("input-validation-error").appendTo(container);
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
