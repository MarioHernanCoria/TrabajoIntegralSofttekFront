function setCookie(nombre, valor, dias) {
    var expiracion = "";
    if (dias) {
        var fecha = new Date();
        fecha.setTime(fecha.getTime() + (dias * 24 * 60 * 60 * 1000));
        expiracion = "; expires=" + fecha.toUTCString();
    }

    document.cookie = nombre + "=" + (valor || "") + expiracion + "; path=/"
}

function getCookie(nombre) {
    var nombre = nombre + "=";
    var coockies = document.cookie.split(";");

    for (var i = 0; i < coockies.length; i++) {
        var c = coockies[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nombre) == 0) return c.substring(nombre.length, c.length)
    }
}