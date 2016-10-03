namespace RepositoryBLL
{
    public static class Mensagems
    {
        public static string mensagem { get; set; }

        public static string SemAcesso()
        {
            mensagem = "Usuário sem acesso ao sistema.";
            return mensagem;
        }

        public static string SenhaNaoConferem()
        {
            mensagem = "Usuário ou senha não conferem.";
            return mensagem;
        }

        public static string SenhaNulosVazios()
        {
            mensagem = "Usuário ou senha não podem ser nulos ou vazios.";
            return mensagem;
        }

        public static string SalvasSucesso()
        {
            mensagem = "Informações salvas com sucesso.";
            return mensagem;
        }

        public static string SalvasErro()
        {
            mensagem = "Erro ao salvar informações.";
            return mensagem;
        }

        public static string ExcluidasSucesso()
        {
            mensagem = "Informações excluídas com sucesso.";
            return mensagem;
        }

        public static string ExcluidasErro()
        {
            mensagem = "Erro ao excluir informações.";
            return mensagem;
        }

        public static string AtualizadasErro()
        {
            mensagem = "Erro ao atualizar informações.";
            return mensagem;
        }

        public static string AtualizadasSucesso()
        {
            mensagem = "Informações atualizadas com sucesso.";
            return mensagem;
        }

        public static string SemInformacoes()
        {
            mensagem = "Desculpe, Não há informações para serem visualizadas.";
            return mensagem;
        }

        public static string CepNaoEncontrado()
        {
            mensagem = "Desculpe, Cep não encontrado.";
            return mensagem;
        }

        public static string FalhaLogon()
        {
            mensagem = "Falha de logon: Nome de usuário desconhecido ou senha incorreta.";
            return mensagem;
        }
    }
}
