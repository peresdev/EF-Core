using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                using var db = new Data.ApplicationContext();
                db.Database.Migrate();

                var existe = db.Database.GetPendingMigrations().Any();

                if (existe)
                {
                
                }
            */

            //InserirDados();
            //InserirDadosEmMassa();
            ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            //RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente
            {
                Id = 3
            };
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente desconhecido 2",
                Telefone = "76555698885"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //cliente.Nome = "Leandro Peres Atualizado";

            //db.Entry(cliente).State = EntityState.Modified;

            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                            .Include(p => p.Itens)
                            .ThenInclude(p => p.Produto)
                            .ToList();

            WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                                    .Where(p => p.Id > 0)
                                    .OrderBy(p => p.Id)
                                    .ToList();

            foreach(var cliente in consultaPorMetodo)
            {
                WriteLine($"Consultando Cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Leandro Peres",
                CEP = "99999900",
                Cidade = "Bauru",
                Estado = "SP",
                Telefone = "99155555554"
            };

            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "Cliente Teste 01",
                    CEP = "99999900",
                    Cidade = "Bauru",
                    Estado = "SP",
                    Telefone = "99155555554"
                },
                new Cliente
                {
                    Nome = "Cliente Teste 02",
                    CEP = "99999900",
                    Cidade = "Bauru",
                    Estado = "SP",
                    Telefone = "99155555554"
                }
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);
            //db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();

            WriteLine($"Total registro(s): {registros}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            //db.Add(produto);

            var registros = db.SaveChanges();
            WriteLine($"Total registro(s): {registros}");
        }
    }
}
