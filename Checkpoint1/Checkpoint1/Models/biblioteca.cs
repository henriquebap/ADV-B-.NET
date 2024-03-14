using System.Collections.Generic;

public class Biblioteca
{
    public Biblioteca()
    {
        UsuariosRegistrados = new List<Usuario>(); // Inicializa UsuariosRegistrados no construtor
        LivrosDisponiveis = new List<Livro>(); // Também é uma boa prática inicializar LivrosDisponiveis
    }

    private List<Livro> LivrosDisponiveis { get; set; }
    private List<Usuario> UsuariosRegistrados { get; set; }

    // Método para acessar a lista de usuários registrados
    public List<Usuario> GetUsuariosRegistrados()
    {
        return UsuariosRegistrados;
    }

    internal bool VerificarDisponibilidade(Livro livro)
    {
        return true;
    }
}
