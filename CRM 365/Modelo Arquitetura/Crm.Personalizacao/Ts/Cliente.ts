namespace CRMSAD {
    export class Cliente {
        public OnLoad(): void
        {
            alert('teste');
        }
    }
}

var cli = new CRMSAD.Cliente();
cli.OnLoad();