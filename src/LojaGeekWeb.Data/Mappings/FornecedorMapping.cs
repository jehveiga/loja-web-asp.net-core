using LojaGeek.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaGeekWeb.Data.Mappings
{
    // Configuração fluent api
    // A classe que possui as outras filhas que configura o relacionamento 
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder) // Configuração do mapeamento da tabela fornecedor
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Fornecedor : Endereco
            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Fornecedor);

            // 1 : N => Fornecedor : Produtos
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);


            builder.ToTable("Fornecedores"); // Configurando o nome da tabela no banco de dados
        }
    }
}
