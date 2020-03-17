using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoName.Shared.Utils
{
    public static class Constantes
    {
        public const string RoutePrefix = "api/v{version:apiVersion}/serviconame";
        public const string VariavelLambda = "x";
        public const string VariavelLambdaPonto = VariavelLambda + ".";
        public const string NomeDependenciaMediator = "Mediator";

        public static class Comparadores
        {
            public const string Igual = " == ";
            public const string Menor = " < ";
            public const string MenorIgual = " <= ";
            public const string Maior = " > ";
            public const string MaiorIgual = " >= ";
        }

        public static class Boleto
        {
            public const string Empresa = "MRVH";
            public const string CodigoParcela = "E001";
            public const string ParcelaEntrada = "X";
            public const string OrganizacaoVenda = "MRVH";

            public const int D2 = 2;
        }
    }
}
