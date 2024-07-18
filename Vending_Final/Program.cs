﻿using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

// Trabalho de Alexandre Martins, Filipe Mota e Ricardo Costa

namespace Vending_Machine
{
    internal class Program
    {
        //************************************************************* FUNÇÕES ***********************************************************************

        //********************************************************** Tabela Produtos ******************************************************************

        static void tabelaProdutos()
        {
            // -------------------------------------------------------------------------------- MATRIZ DE PRODUTOS ------------------------------------------------------------------------------------------
            string[,] menu =
            {
                        { "1","Coca Cola", "1.20" },
                        { "2","Água","1.30" },
                        { "3","Sumo", "1.50" },
                        { "4", "Café", "1.20"},
                        { "5", "Vinho", "1.30" },
                        { "6","Cerveja", "1" },
                        };

            Console.WriteLine("------------------------------------------- Tabela Produtos ------------------------------------------");
            Console.WriteLine("");

            for (int i = 0; i < menu.GetLength(0); i++)
            {

                Console.WriteLine(menu[i, 0] + " " + (menu[i, 1] + " " + (menu[i, 2] + " euros")));

            }

        }

        //************************************************************** Area Admin **********************************************************

        static void areaAdmin(string[,] menu)
        {
            int passwordFlag = 0;
            int adminFlag = 0;

            while (adminFlag == 0)
            {

                Console.WriteLine("Entrar na área de Administrador? ");
                Console.WriteLine("");
                Console.WriteLine("[S] para Sim");
                Console.WriteLine("[N] para Não");
                string admin = Console.ReadLine();


                if (admin == "S" || admin == "s")
                {

                    while (passwordFlag == 0)
                    {
                        passwordFlag = adminPassword();  //Função password para validar adinistrador

                    }

                    if (passwordFlag == 1)
                    {
                        //Caso seja admin Permite Introdução de Nome de bebidas e Preço com validação de valores -------------------------------------------------------------
                        Console.Clear();
                        tabelaProdutosModificada(menu);
                    }
                    Console.Clear();

                }
                else if (admin == "N" || admin == "n")
                {
                    //Continue to User
                    adminFlag = 1;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Caractere inválido");
                    Console.WriteLine("");
                }


            }

        }

        //**************************************************** Area Admin - Alterar Produtos **********************************************************

        static void tabelaProdutosModificada(string[,] menu)
        {
            int alterarTodosLoop = 0;
            int indexint = 0;

            for (int i = 0; i < menu.GetLength(0); i++)
            {
                Console.WriteLine($"{menu[i, 1]}\t{menu[i,2]}");
            }

            Console.WriteLine("");
            Console.WriteLine("Alterar Produtos?");
            Console.WriteLine("");
            Console.WriteLine("[T] para Todos ");
            Console.WriteLine("[I] Alterar Produto individual");
            Console.WriteLine("[X] Cancelar");
            Console.WriteLine("");
            string alterartodos = Console.ReadLine();

            while (alterarTodosLoop == 0)
            {

                if (alterartodos == "T" || alterartodos == "t")
                {
                    Console.Clear ();
                    for (int i = 0; i < menu.GetLength(0); i++)
                    {
                        Console.WriteLine($"{menu[i, 1]}\t{menu[i, 2]}");
                    }

                    for (int i = 0; i < menu.GetLength(0); i++)
                    {
                        Console.WriteLine("Insira o novo nome do produto " + menu[i, 0]);
                        menu[i, 1] = Console.ReadLine();

                        Console.WriteLine("Insira o novo preço do produto " + menu[i, 0]);
                        menu[i, 2] = Console.ReadLine();
                        menu[i, 2] = validacaoDouble(menu[i, 2]);
                    }

                    Console.WriteLine("************************ Tabela Produtos *********************************");
                    Console.WriteLine("");

                    for (int i = 0; i < menu.GetLength(0); i++)
                    {

                        Console.WriteLine(menu[i, 0] + " " + (menu[i, 1] + " " + (menu[i, 2] + " euros")));

                    }

                    Console.WriteLine("Quer alterar Novamente?");
                    Console.WriteLine("[T] para Todos ");
                    Console.WriteLine("[I] Alterar Produto individual");
                    Console.WriteLine("[X] Cancelar");
                    alterartodos = Console.ReadLine();

                }
                else if (alterartodos == "i" || alterartodos == "I") //Permite seleccionar o produto a alterar  
                {

                    Console.Clear();
                    for (int i = 0; i < menu.GetLength(0); i++)
                    {
                        Console.WriteLine($"{menu[i, 1]}\t{menu[i, 2]}");
                    }

                    do //loop validação opçoes entre 1 a 6
                    {
                        Console.WriteLine("Que produto quer alterar. Selecione [1] a [6]");
                        string index = Console.ReadLine();
                        indexint = validacaoInt(index) - 1; //Validação de inteiros

                    } while (indexint < 0 || indexint > 5);


                    Console.WriteLine("Insira o novo nome do produto");
                    menu[indexint, 1] = Console.ReadLine();

                    Console.WriteLine("Insira o novo preço do produto");
                    menu[indexint, 2] = Console.ReadLine();
                    menu[indexint, 2] = validacaoDouble(menu[indexint, 2]);
                    Console.WriteLine("");

                    Console.Clear();
                    for (int i = 0; i < menu.GetLength(0); i++)
                    {
                        Console.WriteLine($"{menu[i, 1]}\t{menu[i, 2]}");
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Quer alterar Novamente?");
                    Console.WriteLine("[T] para Todos ");
                    Console.WriteLine("[I] Alterar Produto individual");
                    Console.WriteLine("[X] Cancelar");
                    alterartodos = Console.ReadLine();

                }
                else if (alterartodos == "X" || alterartodos == "x")
                {
                    alterarTodosLoop = 1;
                }

                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("[T] para Todos ");
                    Console.WriteLine("[I] Alterar Produto individual");
                    Console.WriteLine("[X] Cancelar");
                    alterartodos = Console.ReadLine();
                    //manter no loop while alterarTodosLoop
                }

            }

        }

        //********************************************************* Validação de Inteiros *************************************************************
        static int validacaoInt(string y)
        {
            int validaFlag = 1;


            while (validaFlag != 0)
            {
                if (int.TryParse(y, out int x))
                {

                    validaFlag = 0;
                }
                else
                {
                    Console.WriteLine("caractere inválido");
                    y = Console.ReadLine();
                }
            }
            int yInt = int.Parse(y);

            return yInt;
        }

        //**************************************************** Validação de Doubles *******************************************************************
        // nesta fase os valores são apenas validados e não convertidos em doubles porque a matriz é de strings. a conversão é feita nos cálculos.
        static string validacaoDouble(string y)
        {
            int validaFlag = 1;

            while (validaFlag != 0)
            {
                if (double.TryParse(y, out double x))
                {
                    validaFlag = 0;
                }
                else
                {
                    Console.WriteLine("caractere inválido. Apenas numeros são válidos");
                    y = Console.ReadLine();
                }
            }

            return y;
        }

        //************************************************************* Password **********************************************************************
        static int adminPassword()
        {

            Console.WriteLine("");
            Console.WriteLine("Introduza Password ");
            Console.WriteLine("[X] para sair");
            Console.WriteLine("");

            string password = Console.ReadLine();


            if (password == "1234")
            {
                return 1;

            }
            else if (password == "X" || password == "x")
            {
                return 2;
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Password errada. Tente novamente");
                Console.WriteLine("");
                return 0;
            }


        }

        //************************************************************* Menu Introduza dinheiro ******************************************************

        static void menuIntroDinheiro()
        {
            Console.WriteLine("--------------------------------------- Introduza Dinheiro ------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Máquina apenas aceita: ");
            Console.WriteLine("0.10 euros");
            Console.WriteLine("0.20 euros");
            Console.WriteLine("0.50 euros");
            Console.WriteLine("1 euro");
            Console.WriteLine("2 euros");
            Console.WriteLine("5 euros ");
            Console.WriteLine("");
        }



        //*********************************************************************************************************************************************
        //************************************************************* PROGRAMA **********************************************************************
        //*********************************************************************************************************************************************
        static void Main(string[] args)
        {
            // ----------------------------------------------------- MATRIZ DE PRODUTOS ------------------------------------------------------------------------------------------
            string[,] menu =
            {
                        { "1","Cola", "\t 1.20" },
                        { "2","Água","\t 1.30" },
                        { "3","Sumo", "\t 1.50" },
                        { "4", "Café", "\t 1.20"},
                        { "5", "Vinho", "\t 1.30" },
                        { "6","Cerveja", "\t 1" },
                        };


            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------- INICIO DO PROGRAMA -------------------------------------------------------------------------------------------
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            int voltaInicio = 0;
            while (voltaInicio == 0)
            {
                Console.WriteLine("------------------------------------------- Bem Vindo ------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------- Vending Boa Vida S.A. ----------------------------------------------");
                Console.WriteLine("");

                areaAdmin(menu);
                
                //---------------------------------------------------INICIO DO PROGRAMA DO LADO DO UTILIZADOR FINAL--------------------------------------------------------------

                string validInt = "";
                int validNumeric = 0;
                string admin = "";
                string password = "";
                int passwordFlag = 0;
                int adminFlag = 0;
                double valorTotal = 0;
                double troco = 0;
                string codigo = "";
                int quantidade = 0;
                double valor = 0;
                //double preco = 0;
                int alteracaoProd = 0;
                string index = "";
                int indexint = 0;
                string alteracaopreco = "";
                string alterartodos = "";
                int alterarTodosLoop = 0;

                double valorParcial = 0;
                string valorParcialString = "";
                
                double preco = 1;

                int escolha = 0;
                int consulta;
                int multiplasCompras = 0;
                int avancaCompra = 0;
                int avancaCompraValor = 0;
                int menuConsulta = 0;
                int menuCompra = 0;
                int valorInsuf = 0;
                int multiCompra = 0;


                //----------------------------------------------- Consulta de bebidas e preços ----------------------------------------------------------------------------------

                while (avancaCompra == 0)
                {
                    for (int i = 0; i < menu.GetLength(0); i++)
                    {

                        Console.WriteLine("[" + (menu[i, 0] + "]" + menu[i, 1]));

                    }
                    Console.WriteLine("------------------------------- Consulte Preços ---------------------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine(" [1] a [6] Consultar Preços");
                    Console.WriteLine(" [7] Avançar com a compra");
                    Console.WriteLine("");
                    validInt = Console.ReadLine();
                    Console.Clear();
                    // Validação de Inteiros. Não permite introdução de letras ou outros caracteres   
                    menuConsulta = validacaoInt(validInt);


                    //Menu de Consulta de preços e produtos --------------------------------------------
                    switch (menuConsulta)
                    {
                        case 1:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " +menu[0, 1]);
                            Console.WriteLine("Preço    " + menu[0, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 2:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " + menu[1, 1]);
                            Console.WriteLine("Preço    " + menu[1, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 3:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " + menu[2, 1]);
                            Console.WriteLine("Preço    " + menu[2, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 4:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " + menu[3, 1]);
                            Console.WriteLine("Preço    " + menu[3, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 5:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " + menu[4, 1]);
                            Console.WriteLine("Preço    " + menu[4, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 6:
                            Console.WriteLine("");
                            Console.WriteLine("Escolheu " + menu[5, 1]);
                            Console.WriteLine("Preço    " + menu[5, 2] + " euros");
                            Console.WriteLine("");
                            break;
                        case 7:
                            avancaCompra = 1;
                            break;
                    }
                }

                //Introdução de moedas e calculo do valor total - Fase Inicial - -------------------------------------------

                while (valorInsuf == 0)
                {
                    menuIntroDinheiro();

                    //Introdução de moedas e calculo do valor total - Introdução de moedas sem limite - -------------------------

                    avancaCompraValor = 0;
                    while (avancaCompraValor == 0)
                    {
                        valorParcialString = Console.ReadLine();
                       
                        if (valorParcialString == "0.1" || valorParcialString == "0.2" || valorParcialString == "0.5" || valorParcialString == "1" || valorParcialString == "2" || valorParcialString == "5") // Validação de Doubles. Não permite introdução de letras ou outros caracteres  ------------------------------------
                        {
                            
                            Console.Clear();
                            valorParcial = double.Parse(valorParcialString, CultureInfo.InvariantCulture);
                            valorParcial = Math.Round(valorParcial, 2);

                            valorTotal = Math.Round((valorTotal + valorParcial), 2);
                            menuIntroDinheiro();
                            Console.WriteLine("");
                            Console.WriteLine("O seu saldo é de " + valorTotal + " euros");
                            Console.WriteLine("");
                            Console.WriteLine("Adicione mais saldo");
                            Console.WriteLine("ou ");
                            Console.WriteLine("[7] Avançar ");
                            Console.WriteLine("[0] Cancelar e receber o saldo");
                            Console.WriteLine("");
                            
                            multiCompra = 0;

                        }

                        else if (valorParcialString == "0") //Cancelar compra e receber valor em saldo
                        {
                            Console.Clear();
                        
                            multiCompra = 1;
                            avancaCompraValor = 1;
                            valorInsuf = 1;
                            menuCompra = 1;
                        }

                        else if (valorParcialString == "7") //Avançar para a compra
                        {
                            Console.Clear();
                            avancaCompraValor = 1;

                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Valor introduzido inválido. Tente novamente");
                            Console.WriteLine("");
                        }                    
                    }

                    //Escolha de produto para comprar --------------------------------------------------------------------
 
                    while (multiCompra == 0)
                    {
                        Console.WriteLine("--------------------------------------- Escolha a Bebida ------------------------------------");
                        Console.WriteLine("");

                        //Validação de Inteiros.Não permite introdução de letras ou outros caracteres na escolha do Menu
                        validNumeric = 0;
                        while (validNumeric == 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("[1] a [6] Escolher a bebida");
                            Console.WriteLine("[7] Inserir mais Saldo");
                            Console.WriteLine("[8] Terminar compra e recolher troco");
                            Console.WriteLine("");

                            validInt = Console.ReadLine();

                            menuCompra = validacaoInt(validInt);//Validação caracteres

                            //Escolha de bebiba para comprar ----------------------------------------------------------------------------
                            
                            Console.Clear();
                            valorTotal = Math.Round(valorTotal, 2);
                            
                            switch (menuCompra)
                            {
                                case 1:
                                    Console.WriteLine("Escolheu " + menu[0, 1]);
                                    Console.WriteLine("Preço " + menu[0, 2] + " euros");

                                    if (valorTotal < Convert.ToDouble(menu[0, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;
                                    }
                                    else
                                    {

                                        valorTotal = valorTotal - (Convert.ToDouble(menu[0, 2], CultureInfo.InvariantCulture));
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }

                                    break;
                                case 2:
                                    Console.WriteLine("Escolheu " + menu[1, 1]);
                                    Console.WriteLine("Preço " + menu[1, 2] + " euros");
                                    if (valorTotal < Convert.ToDouble(menu[1, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal - Convert.ToDouble(menu[1, 2], CultureInfo.InvariantCulture);
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("Escolheu " + menu[2, 1]);
                                    Console.WriteLine("Preço " + menu[2, 2] + " euros");
                                    if (valorTotal < Convert.ToDouble(menu[2, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal - Convert.ToDouble(menu[2, 2], CultureInfo.InvariantCulture);
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("Escolheu " + menu[3, 1]);
                                    Console.WriteLine("Preço " + menu[3, 2] + " euros");
                                    if (valorTotal < Convert.ToDouble(menu[3, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal - Convert.ToDouble(menu[3, 2], CultureInfo.InvariantCulture);
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }
                                    break;
                                case 5:
                                    Console.WriteLine("Escolheu " + menu[4, 1]);
                                    Console.WriteLine("Preço " + menu[4, 2] + " euros");
                                    if (valorTotal < Convert.ToDouble(menu[4, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal - Convert.ToDouble(menu[4, 2], CultureInfo.InvariantCulture);
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }
                                    break;
                                case 6:
                                    Console.WriteLine("Escolheu " + menu[5, 1]);
                                    Console.WriteLine("Preço " + menu[5, 2] + " euros");
                                    if (valorTotal < Convert.ToDouble(menu[5, 2], CultureInfo.InvariantCulture))
                                    {
                                        Console.WriteLine("O seu salde é de " + valorTotal + " euros que é insuficiente para esta compra");
                                        Console.WriteLine("Introduza mais valor");
                                        valorParcial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                        multiCompra = 1;
                                        valorInsuf = 0;

                                    }
                                    else
                                    {
                                        valorTotal = valorTotal - Convert.ToDouble(menu[5, 2], CultureInfo.InvariantCulture);
                                        valorTotal = Math.Round(valorTotal, 2);
                                        Console.WriteLine("O seu saldo é de " + valorTotal);

                                    }
                                    break;

                                case 7: //Inserir mais dinheiro -----------------------------------------------------------------------------------

                                    multiCompra = 1;
                                    valorInsuf = 0;
                                    validNumeric = 1;
                                    break;

                                case 8: //Desistir da compra e receber o valor em saldo -----------------------------------------------------------

                                    valorInsuf = 1;
                                    multiCompra = 1;
                                    validNumeric = 1;
                                    break;
                            }
                        }
                    }
                }


                //Cálculo do Troco

                int[] contadores = new int[6];
                double[] valores = { 5.0, 2.0, 1.0, 0.5, 0.2, 0.1 };

                // Loop while para subtrair o valor correspondente de valorTotal e incrementar o contador apropriado.
                for (int i = 0; i < valores.Length; i++)
                {
                    while (valorTotal >= valores[i])
                    {
                        valorTotal = valorTotal - valores[i];
                        contadores[i]++;
                    }
                    valorTotal = Math.Round(valorTotal, 2);
                }

                // Atribuição dos contadores e calculo do troco

                int contador5 = contadores[0];
                int contador2 = contadores[1];
                int contador1 = contadores[2];
                int contador05 = contadores[3];
                int contador02 = contadores[4];
                int contador01 = contadores[5];

                troco = contador5 * 5 + contador2 * 2 + contador1 * 1 + contador05 * 0.5 + contador02 * 0.2 + contador01 * 0.1;
                troco = Math.Round(troco, 2);
                valorTotal = Math.Round(valorTotal, 2);

                Console.WriteLine("----------------------------- Relatório Final ----------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Recebeu " + troco + " Euros");
                Console.WriteLine("Foram devolvidas:");
                Console.WriteLine(contador5 + " nota(s) de 5 euros");
                Console.WriteLine(contador2 + " moeda(s) de 2 euros");
                Console.WriteLine(contador1 + " moeda(s) de 1 euro");
                Console.WriteLine(contador05 + " moeda(s) de 50 centimos");
                Console.WriteLine(contador02 + " moedas de 20 centimos");
                Console.WriteLine(contador01 + " moedas de 10 centimos");
                Console.WriteLine("");
                Console.WriteLine("O seu saldo é de " + valorTotal + " euros");
                Console.WriteLine("Volte sempre");
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------- Obrigado pela Compra ------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------ Volte Sempre ----------------------------------------------");
                Console.WriteLine("");


                System.Threading.Thread.Sleep(10000);
                Console.Clear();

                avancaCompraValor = 1;
                voltaInicio = 0;
            }

        }
    }
}


