﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public enum EstadoLivro
{
    Disponível,
    Reservado,
    Emprestado,
    Danificado,
    Perdido
}