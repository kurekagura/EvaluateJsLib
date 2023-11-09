function toggleSidebar(toggleId, menuId, avatarId) {
    const aElm = document.getElementById(toggleId);
    const menuElm = document.getElementById(menuId);
    const avatarElm = document.getElementById(avatarId);

    var spanElms = menuElm.querySelectorAll('li.nav-item a.nav-link span');

    spanElms.forEach(function (spanElm) {
        var hasDNoneClass = spanElm.classList.contains('d-none');
        if (hasDNoneClass) {
            spanElm.classList.remove('d-none');
        } else {
            spanElm.classList.add('d-none');
        }
    });
    var spanAvaElm = avatarElm.querySelector('span');
    if (spanAvaElm.classList.contains('d-none')) {
        spanAvaElm.classList.remove('d-none');
    } else {
        spanAvaElm.classList.add('d-none');
    }

    var iElm = aElm.querySelector('i');
    if (iElm.classList.contains('bi-arrows-angle-contract')) {
        iElm.classList.replace('bi-arrows-angle-contract', 'bi-arrows-angle-expand');
        document.cookie = 'sidebar_dnone=d-none;path=/;';
        console.log('set d-none');
    } else if (iElm.classList.contains('bi-arrows-angle-expand')) {
        iElm.classList.replace('bi-arrows-angle-expand', 'bi-arrows-angle-contract');
        document.cookie = 'sidebar_dnone=;path=/;';
        console.log('clear d-none');
    }
}