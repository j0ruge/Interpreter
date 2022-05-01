using System;

namespace Interpreter
{
    class Program
    {
        static int menu_op = 0;                 //valor para seleção do menu
        static string code_op = "000";          //valor para seleção da operação
        static short registrador_a = 0;         //registrador A
        static short registrador_b = 0;         //registrador B
        static char reg_op = 'A';               //usado para recursividade das seleções de registrador

        static bool run_bit = true;            // um bit que pode ser desligado para parar a máquina

        const int CLR = 90;// <-- seta o valor no accumulator para 0
        const int ADDI = 95;// <-- adiciona o valor x no accumulator
        const int ADDM = 93;// <-- adiciona o valor da memória y no accumulator
        const int HALT = 100;// <-- instrução que desliga o processador

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            while (run_bit)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Menu Principal da ULA");
                Console.WriteLine("");
                Console.WriteLine("  1. Definir registrador A");
                Console.WriteLine("  2. Definir registrador B");
                Console.WriteLine("  3. Ler registrador A (Acc)");
                Console.WriteLine("  4. Ler registrador B");
                Console.WriteLine("  5. Ler registrador de flags");
                Console.WriteLine("  6. Definir operação");
                Console.WriteLine("  7. Executar ULA");
                Console.WriteLine("  8. Sair");
                Console.WriteLine("");
                Console.WriteLine("Escolha uma opção =>");

                var retorno = Console.ReadLine();

                if (retorno.Length == 0 || int.Parse(retorno) < 1 || int.Parse(retorno) > 8)
                    Menu();
                else
                    menu_op = int.Parse(retorno);

                switch (menu_op)
                {
                    case 1:
                        Console.Clear();
                        reg_op = 'A';
                        DefinirRegistrador(reg_op);
                        break;
                    case 2:
                        Console.Clear();
                        reg_op = 'B';
                        DefinirRegistrador(reg_op);
                        break;
                    case 3:
                        Console.Clear();
                        reg_op = 'A';
                        LerRegistrador(reg_op);
                        break;
                    case 4:
                        Console.Clear();
                        reg_op = 'B';
                        LerRegistrador(reg_op);
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Ler registrador de flags");
                        break;
                    case 6:
                        SelecionarOperacao();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Executar ULA");
                        Execute();
                        Console.WriteLine("Aperte qualquer tecla para continuar");
                        Console.ReadLine();
                        break;
                    case 8:
                        Console.WriteLine("");
                        Console.WriteLine("HALT");
                        run_bit = false;
                        break;
                }
            }
        }

        static void DefinirRegistrador(char reg)
        {
            if(reg == 'A')
            {
                Console.WriteLine("Definir registrador A => -32768 até 32767");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador(reg_op);
                }
                else
                {
                    registrador_a = short.Parse(retorno);
                }   
            }
            else
            {
                Console.WriteLine("Definir registrador B => -32768 até 32767");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador(reg_op);
                }
                else
                {
                    registrador_b = short.Parse(retorno);
                }
            }
        }

        static void LerRegistrador(char reg)
        {
            if (reg == 'A')
            {
                Console.Clear();
                Console.WriteLine("Ler registrador A");
                Console.WriteLine($"Valor: {registrador_a}");
                Console.WriteLine("");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ler registrador B");
                Console.WriteLine($"Valor: {registrador_b}");
                Console.WriteLine("");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadLine();
            }
        }

        static void SelecionarOperacao()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Definir operação");
            Console.WriteLine("");
            Console.WriteLine("  1. A");
            Console.WriteLine("  2. B");
            Console.WriteLine("  3. A + 1");
            Console.WriteLine("  4. B + 1");
            Console.WriteLine("  5. A + B");
            Console.WriteLine("  6. A - B");
            Console.WriteLine("  7. A and B");
            Console.WriteLine("  8. A or B");
            Console.WriteLine("");
            Console.WriteLine("Escolha uma opção =>");

            var retorno = Console.ReadLine();

            if (retorno.Length == 0 || int.Parse(retorno) < 1 || int.Parse(retorno) > 8)
                SelecionarOperacao();
            else
            {
                switch (int.Parse(retorno))
                {
                    case 1:
                        code_op = "000";
                        break;
                    case 2:
                        code_op = "001";
                        break;
                    case 3:
                        code_op = "010";
                        break;
                    case 4:
                        code_op = "011";
                        break;
                    case 5:
                        code_op = "100";
                        break;
                    case 6:
                        code_op = "101";
                        break;
                    case 7:
                        code_op = "110";
                        break;
                    case 8:
                        code_op = "111";
                        break;
                }
            }
                
        }

        private static void Execute()
        {
            switch (code_op)
            {
                case "000":
                    break;
                case "001":
                    break;
                case "010":
                    break;
                case "011":
                    break;
                case "100":
                    Soma();
                    break;
                case "101":
                    break;
                case "110":
                    break;
                case "111":
                    break;
            }
        }

        private static void Soma()
        {
            var output = registrador_a + registrador_b;
            ClassificarFlag(output);
            registrador_a = (short) output;
        }

        private static void ClassificarFlag(int valor)
        {

        }
    }
}
