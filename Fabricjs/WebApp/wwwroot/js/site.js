// クロップ後の背景は？
//const rect = { left: 50, top: 50, width: 100, height: 100 }; // クロップする範囲を設定
//function cropImage(originalImage, rect) {
//    const croppedCanvas = document.createElement('canvas');

//    croppedCanvas.width = rect.width;
//    croppedCanvas.height = rect.height;

//    const context = croppedCanvas.getContext('2d');

//    context.drawImage(originalImage, rect.left, rect.top, rect.width, rect.height, 0, 0, rect.width, rect.height);

//    const dataUrl = croppedCanvas.toDataURL('image/png');
//    return dataUrl;
//}

//rect領域を黒塗り（Mask）する
function createCircleMaskImage(originalImage, origin_width, origin_height, circle) {
    const maskCanvas = document.createElement('canvas');
    maskCanvas.width = origin_width;
    maskCanvas.height = origin_height;

    const context = maskCanvas.getContext('2d');

    // 全体を元画像で塗りつぶす
    context.drawImage(originalImage, 0, 0, origin_width, origin_height);

    // クロップ領域を明示的に黒で塗りつぶす
    //context.fillStyle = 'black';
    //context.fillRect(rect.left, rect.top, rect.width, rect.height);

    context.beginPath();
    context.arc(circle.centerX, circle.centerY, circle.radius, 0, 2 * Math.PI);
    context.fillStyle = 'black';
    context.fill();
    context.closePath();

    const maskDataUrl = maskCanvas.toDataURL('image/png');
    return maskDataUrl;
}

function onModifiedCircle(options, imgObj, origin_width, origin_height, scaleFromOriginal, $preview) {
    const modifiedCircle = options.target;

    // 半径、中心座標を取得
    //const radius = modifiedCircle.radius; //一定（変化しない）
    const scale = modifiedCircle.scaleX;
    const scaleY = modifiedCircle.scaleY;
    const radius = (modifiedCircle.width * scale) / 2;
    const radiusH = modifiedCircle.height / 2;

    const centerX = modifiedCircle.left + radius;
    const centerY = modifiedCircle.top + radius;

    const inv_scale = 1 / scaleFromOriginal;

    const circleMask = { centerX: centerX * inv_scale, centerY: centerY * inv_scale, radius: radius * inv_scale };
    console.log('circleMask', circleMask);
    const previewDataUrl = createCircleMaskImage(imgObj, origin_width, origin_height, circleMask);
    $preview.attr('src', previewDataUrl);
}

function createMaskFabricCircle(left, top, radius) {

    const circle = new fabric.Circle({
        left: left,
        top: top,
        radius: radius,
        fill: 'transparent',
        stroke: 'red',
        strokeWidth: 1,
        strokeUniform: true,
        selectable: true,
        hasControls: true
    });
    circle.setControlVisible('ml', false); // 左中央のコントロールを非表示
    circle.setControlVisible('mt', false); // 上中央のコントロールを非表示
    circle.setControlVisible('mr', false); // 右中央のコントロールを非表示
    circle.setControlVisible('mb', false); // 下中央のコントロールを非表示
    circle.setControlVisible('mtr', false); // 回転コントロールを非表示
    return circle;
}

//rect領域を黒塗り（Mask）する
function createRectMaskImage(originalImage, origin_width, origin_height, rect) {
    const maskCanvas = document.createElement('canvas');
    maskCanvas.width = origin_width;
    maskCanvas.height = origin_height;

    const context = maskCanvas.getContext('2d');

    // 全体を元画像で塗りつぶす
    context.drawImage(originalImage, 0, 0, origin_width, origin_height);

    // マスク領域を明示的に黒で塗りつぶす
    context.fillStyle = 'black';
    context.fillRect(rect.left, rect.top, rect.width, rect.height);

    const maskDataUrl = maskCanvas.toDataURL('image/png');
    return maskDataUrl;
}

function onModifiedRect(options, imgObj, origin_width, origin_height, scale, $preview) {
    console.log('modified');
    const t = options.target;
    console.log(t.aCoords);
    console.log(t.lineCoords);
    // const c = t.aCoords;
    const c = t.lineCoords;

    const left = c.tl.x;
    const top = c.tl.y;
    const width = c.tr.x - c.tl.x;
    const height = c.bl.y - c.tl.y;
    console.log(`left:${left},top:${top},width:${width},height:${height}`);

    //掛け値が重要
    const inv_scale = 1 / scale;
    console.log('inverse scale', inv_scale);
    const cropRect = { left: left * inv_scale, top: top * inv_scale, width: width * inv_scale, height: height * inv_scale };

    const previewDataUrl = createRectMaskImage(imgObj, origin_width, origin_height, cropRect);
    //img要素に設定
    $preview.attr('src', previewDataUrl);
    console.log('cropRect(サーバ取得時の原画基準)', cropRect);
}

function createMaskFabricRect(left, top, width, height) {
    var rect = new fabric.Rect({
        left: left,
        top: top,
        width: width,
        height: height,
        fill: 'transparent',
        stroke: 'red',
        strokeWidth: 1,
        strokeUniform: true,
        lockRotation: true
    });
    rect.setControlVisible('mtr', false); // 回転コントロールを非表示
    return rect;
}

//クロップ領域以外は黒で埋める
function createCropImage(originalImage, origin_width, origin_height, rect) {
    const croppedCanvas = document.createElement('canvas');

    //サーバから取得時（元画像）のサイズが必須。
    console.log('クロップ用canvas設定サイズ', origin_width, origin_height);
    croppedCanvas.width = origin_width;
    croppedCanvas.height = origin_height;

    const context = croppedCanvas.getContext('2d');

    // まずはキャンバス全体を黒で塗りつぶす
    context.fillStyle = 'black';
    context.fillRect(0, 0, croppedCanvas.width, croppedCanvas.height);

    // クロップ領域だけを元画像から切り出して描画
    context.drawImage(originalImage, rect.left, rect.top, rect.width, rect.height, rect.left, rect.top, rect.width, rect.height);

    const dataUrl = croppedCanvas.toDataURL('image/png');
    return dataUrl;
}

function calcFitSquareRatio(width, height, fitLength) {
    // アスペクト比を計算
    const aspectRatio = width / height;

    // 縦横が fitLength の正方形に一致している場合、拡大・縮小不要
    if (width === fitLength && height === fitLength) {
        return 1;
    }

    // 縦横どちらかが fitLength よりも大きい場合、縮小が必要
    if (width > fitLength || height > fitLength) {
        return fitLength / Math.max(width, height);
    }

    // 縦横どちらも fitLength よりも小さい場合、拡大が必要
    if (width < fitLength && height < fitLength) {
        return fitLength / Math.max(width, height);
    }
}