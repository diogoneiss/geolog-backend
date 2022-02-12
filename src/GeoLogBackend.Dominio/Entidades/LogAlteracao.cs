using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLogBackend.GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using System;
using GeoLogBackend.Dominio;
using GeoLogBackend.GeoLogBackend.Dominio.Entidades.Dtos;

public class LogAlteracao : Entidade, IAggregateRoot
    {

    public LogAlteracao()
    {

    }

    public LogAlteracao(Usuario usuarioQueModificou, Pais paisModificado, InformacaoPaisDto modificacaoFeita)
    {
        UsuarioQueModificou = usuarioQueModificou;
        PaisModificado = paisModificado;
        ModificacaoFeita = new Modificacao(modificacaoFeita);
    }

    public Usuario UsuarioQueModificou { get; set; }
    public Pais PaisModificado { get; set; }

    public Modificacao ModificacaoFeita { get; set; }



}

public class Modificacao : IAggregateRoot
{
    public DateTime Momento { get; set; }
    public string Campo { get; set; }
    public string Valor { get; set; }

    public Modificacao()
    {

    }

    public Modificacao(DateTime momento, string campo, string valor)
    {
        Momento = momento;
        Campo = campo;
        Valor = valor;
    }

    public Modificacao(InformacaoPaisDto ModificacaoFeita)
    {
        Momento = DateTime.Now;
        Campo = ModificacaoFeita.Campo;
        Valor = ModificacaoFeita.Valor.ToString();
    }
}