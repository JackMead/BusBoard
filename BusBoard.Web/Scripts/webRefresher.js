
function timedRefresh() {

    setTimeout(() => {
        location.reload();
    }, 30000);
}

$(document).ready(timedRefresh)
