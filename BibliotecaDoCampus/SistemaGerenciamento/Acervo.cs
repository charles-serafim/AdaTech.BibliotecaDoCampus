using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento;

public enum Acervo
{
    [Description("Acervo Restrito")]
    AcervoRestrito,

    [Description("Acervo Público")]
    AcervoPublico,

    [Description("Fora de Estoque")]
    ForaDeEstoque
}