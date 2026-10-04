using MudBlazor;

namespace afya_admin.Components;

public static class Ui
{
    // Devolve classe de fundo pastel nativa (ex: mud-success-hover)
    public static string FundoSuave(Color cor) => $"mud-{cor.ToString().ToLowerInvariant()}-hover";

    // Gera iniciais de nomes para avatares sem foto
    public static string Iniciais(string nome)
    {
        var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return partes.Length switch
        {
            0 => "?",
            1 => partes[0][..1].ToUpperInvariant(),
            _ => $"{partes[0][0]}{partes[^1][0]}".ToUpperInvariant(),
        };
    }
}
