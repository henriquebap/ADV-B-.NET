using System.Collections.Generic;

public interface IRepositorioLivros
{
    void AdicionarLivro(Livro livro);
    void RemoverLivro(Livro livro);
    Livro BuscarLivro(string isbn);
    List<Livro> ListarLivros();
}
