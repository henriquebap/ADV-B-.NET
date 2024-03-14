using System;
using System.Collections.Generic;

public class Usuario
{
    public Usuario()
    {
        HistoricoEmprestimos = new List<Emprestimo>();
        Nome = string.Empty; // Para evitar o erro CS8618
    }

    public string Nome { get; set; }
    public int ID { get; set; }
    public List<Emprestimo> HistoricoEmprestimos { get; set; }

    // [Obsolete("Este método está obsoleto e não deve ser usado.")]
    protected double CalcularMulta(DateTime dataDevolucao)
    {
        return 0.0;
    }

    public static implicit operator Usuario(List<Usuario> v)
    {
        throw new NotImplementedException();
    }
}
