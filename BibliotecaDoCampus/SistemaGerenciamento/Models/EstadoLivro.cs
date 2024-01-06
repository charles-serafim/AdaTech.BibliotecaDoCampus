using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public enum EstadoLivro
{
    Disponivel = 1,
    Reservado = 2,
    Emprestado = 3,
    Danificado = 4,
    Perdido = 5,
    AguardandoAprovacao = 6
}
