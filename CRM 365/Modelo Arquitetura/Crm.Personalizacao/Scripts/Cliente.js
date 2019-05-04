var CRMSAD;
(function (CRMSAD) {
    var Cliente = /** @class */ (function () {
        function Cliente() {
        }
        Cliente.prototype.OnLoad = function () {
            alert('teste');
        };
        return Cliente;
    }());
    CRMSAD.Cliente = Cliente;
})(CRMSAD || (CRMSAD = {}));
var cli = new CRMSAD.Cliente();
cli.OnLoad();
//# sourceMappingURL=Cliente.js.map