//老sleep方法存在问题
//async function sleep(interval) {
//    return new Promise(resolve => {
//        setTimeout(resolve, interval);
//    })
//}
//await sleep(1000);

(() => { var len1 = document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope').length; return len1 ; })();

//function sleep(d) {
//    for (var t = Date.now(); Date.now() - t <= d;);
//}

//var len1 = document.getElementsByClassName('clearfix module-name module-title').length;
//return len1;
//for (let i = 0; i < len1; i++) {
//    setTimeout(function () {
//        document.getElementsByClassName('clearfix module-name module-title')[i].click();
//    }, i * 200);
//}

//var len2 = document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope').length;
//for (let i = 0; i < len2; i++) {
//    setTimeout(function () {
//        len2 = document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope').length;
//        document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope')[i].click();
//    }, i * 10000);


//    //setTimeout(function () {
//    //    len1 = document.getElementsByTagName('img').length;
//    //}, i*10000);
//}

//function GetFrameState(frm) {
//    var framedoc = frm.document || frm.contentWindow.document;
//    if (framedoc.readyState == 'complete') {
//        var frms = frm.frames;
//        if (frms && frms.length > 0) {
//            for (var i = 0; i < frms.length; i++) {
//                if (frms[i].document.readyState != 'complete') {        //current iframe request unfinished.
//                    console.log("111");
//                    return false;
//                }
//            }
//        }
//    }
//    return true;
//}