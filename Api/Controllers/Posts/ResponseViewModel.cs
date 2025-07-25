﻿using Infraestrutura.ViewModels;
using static Api.Models.Post;

namespace Api.Controllers.Posts;

public class ResponseViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string? Image { get; set; }
    public Guid TipoPostId { get; set; }
    public string TipoNome { get; set; }
    public string Corpo { get; set; }
    public ELiberado Liberado { get; set; }
    public string LiberadoNome { get; set; }
}
