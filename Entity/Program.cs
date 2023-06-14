using Entity.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite:\n" + 
                "1 para criar uma pessoa\n" +
                "2 para alterar o nome da pessoa\n" +
                "3 para inserir email\n" +
                "4 para excluir uma pessoa \n" +
                "5 para consultar TUDO\n" +
                "6 para consultar pelo ID"
                );

            int op = int.Parse( Console.ReadLine() );

            Contexto contexto = new Contexto();

            switch(op)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Insera o nome da pessoa:");
                        Pessoa p = new Pessoa();
                        p.nome = Console.ReadLine();

                        //modo 1
                        Console.WriteLine("Insira um email:");
                        string emailTemp = Console.ReadLine();

                        p.emails = new List<Email>()
                        {
                            new Email()
                            {
                                email = emailTemp
                            }
                        };

                        //modo 2
                        //Email e = new Email();
                        //Console.WriteLine("Insira um email:");
                        //e.email = Console.ReadLine();

                        //p.emails = new List<Email>();
                        //p.emails.Add(e);

                        contexto.Pessoas.Add(p);
                        contexto.SaveChanges();

                        Console.WriteLine("Pessoa inserida com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                break;

                case 2:
                    try
                    {
                        Console.WriteLine("Informe Id da Pessoa: ");
                        int idPessoa = int.Parse(Console.ReadLine());

                        Pessoa? pAlt = contexto.Pessoas.Find(idPessoa);

                        Console.WriteLine("Informe o nome correto: ");
                        pAlt.nome = Console.ReadLine();

                        contexto.Pessoas.Update(pAlt);
                        contexto.SaveChanges();

                        Console.WriteLine("Nome alterado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                break;

                case 3:
                    try
                    {
                        Console.WriteLine("Informe o ID da pessoa: ");
                        int id = int.Parse(Console.ReadLine());

                        Pessoa? p = contexto.Pessoas.Find(id);

                        Console.WriteLine("Informe o novo email: ");
                        string emailTemp = Console.ReadLine();

                        p.emails.Add(new Email()
                        {
                            email = emailTemp
                        });

                        contexto.Pessoas.Update(p);
                        contexto.SaveChanges();

                        Console.WriteLine("Email inserido com sucesso!");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                break;

                case 4:
                    try
                    {
                        Console.WriteLine("Informe o ID para exclusão: ");
                        int id = int.Parse(Console.ReadLine());
                        Pessoa p = contexto.Pessoas.Find(id);

                        Console.WriteLine("Confirmar a exclusão de " + p.nome);
                        Console.WriteLine("E dos seus emails?");

                        foreach (Email item in p.emails)
                        {
                            Console.WriteLine("\t" +  item.email);
                        }

                        Console.WriteLine("1 para SIM e 2 para NÃO");

                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            contexto.Pessoas.Remove(p);
                            contexto.SaveChanges();

                            Console.WriteLine("Excluído com sucesso!");
                        }
                                               
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                break;

                case 5:                                      
                        List<Pessoa> pessoas = (from Pessoa p in contexto.Pessoas select p)
                            .Include(pes => pes.emails).ToList<Pessoa>();

                        foreach (Pessoa item in pessoas)
                        {
                            Console.WriteLine(item.id + " - " + item.nome);

                            foreach (Email itemE in item.emails)
                            {
                                Console.WriteLine("\t" + itemE.email);
                            }
                        }             
       
                break;

                case 6:
                    try
                    {
                        Console.WriteLine("Informe o ID da pessoa: ");
                        int idP = int.Parse(Console.ReadLine());

                        Pessoa pessoa = contexto.Pessoas.Include(p => p.emails).FirstOrDefault(x => x.id == idP);

                        Console.WriteLine(pessoa.id + " - " + pessoa.nome);

                        foreach (Email item in pessoa.emails)
                        {
                            Console.WriteLine("\t" + item.email);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                break;

                default:
                break;
            }
            
        }
    }
}