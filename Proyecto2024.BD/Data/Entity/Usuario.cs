﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2024.BD.Data.Entity
{
    [Index(nameof(Contraseña), Name = "Usuario_UQ", IsUnique = true)]
    public class Usuario : EntityBase
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio.")]
        [MaxLength(8, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "El codigo de clase de documento es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "La persona es obligatorio.")]
        public int PersonaID { get; set; }
        public Persona Persona { get; set; }

        public List<Registro> Registros { get; set; }

        public List<Cita> Citas { get; set; }


    }
}
