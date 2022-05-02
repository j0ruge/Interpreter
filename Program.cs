using System;

namespace Interpreter
{
    class Program
    {
        static int menu_op = 0;                 //valor para seleção do menu
        static string code_op = "0000";          //valor para seleção da operação
        static short registrador_a = 0;         
        static short registrador_b = 0;         
        static char reg_op = 'A';               //usado para recursividade das seleções de registrador

        static bool flag_transbordo = false;
        static bool flag_negativo = false;
        static bool flag_zero = false;

        #region => ALU operation codes (opcodes)
        const string ADDITION = "0100";
        const string SUBTRACTION = "0101";
        const string HALT = "1000";
        const string CLEAR = "0000";
        const string INCREMENT_A = "0010";
        const string INCREMENT_B = "0011";
        const string AND = "0110";
        const string OR = "0111";
        #endregion => ALU operation codes (opcodes)


        static bool run_bit = true;            // um bit que pode ser desligado para parar a máquina

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
                        DefinirRegistrador();
                        break;
                    case 2:
                        Console.Clear();
                        reg_op = 'B';
                        DefinirRegistrador();
                        break;
                    case 3:
                        Console.Clear();
                        reg_op = 'A';
                        LerRegistrador();
                        break;
                    case 4:
                        Console.Clear();
                        reg_op = 'B';
                        LerRegistrador();
                        break;
                    case 5:
                        Console.Clear();
                        RetornarFlags();
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
                        run_bit = false;
                        break;
                }
            }
        }

        static void DefinirRegistrador()
        {
            if(reg_op == 'A')
            {
                Console.WriteLine("Definir registrador A => Valores válidos, -32768 até 32767");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador();
                }
                else
                {
                    registrador_a = short.Parse(retorno);
                }   
            }
            else
            {
                Console.WriteLine("Definir registrador B => Valores válidos, -32768 até 32767");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador();
                }
                else
                {
                    registrador_b = short.Parse(retorno);
                }
            }
        }

        static void LerRegistrador()
        {
            if (reg_op == 'A')
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
            Console.WriteLine("  1. CLEAR");
            Console.WriteLine("  2. B");
            Console.WriteLine("  3. INCREMENT A");
            Console.WriteLine("  4. INCREMENT B");
            Console.WriteLine("  5. ADDITION");
            Console.WriteLine("  6. SUBTRACTION");
            Console.WriteLine("  7. AND");
            Console.WriteLine("  8. OR");
            Console.WriteLine("  9. HALT");
            Console.WriteLine("");
            Console.WriteLine("Escolha uma opção =>");

            var retorno = Console.ReadLine();

            if (retorno.Length == 0 || int.Parse(retorno) < 1 || int.Parse(retorno) > 9)
                SelecionarOperacao();
            else
            {
                switch (int.Parse(retorno))
                {
                    //A
                    case 1:
                        code_op = CLEAR;
                        break;
                    //B
                    case 2:
                        code_op = "0001";
                        break;
                    //
                    case 3:
                        code_op = INCREMENT_A; // Saída A + 1
                        break;                    
                    case 4:
                        code_op = INCREMENT_B; //B + 1
                        break;                    
                    case 5:
                        code_op = ADDITION; //Saída A + B
                        break;                    
                    case 6:
                        code_op = SUBTRACTION; //Saída A - B
                        break;
                    //A and B
                    case 7:
                        code_op = AND;
                        break;
                    //A or B
                    case 8:
                        code_op = OR;
                        break;                    
                    case 9:
                        code_op = HALT;
                        break;
                }
            }
                
        }

        private static void Execute()
        {
            switch (code_op)
            {
                case CLEAR:
                    Clear();
                    break;
                case "0001":
                    break;
                case INCREMENT_A:
                    reg_op = 'A';
                    Incremento();
                    break;
                case INCREMENT_B:
                    reg_op = 'B';
                    Incremento();
                    break;
                case ADDITION:
                    Soma();
                    break;
                case SUBTRACTION:
                    Subtrair();
                    break;
                case AND:
                    break;
                case OR:
                    break;
                case HALT:
                    Halt();
                    break;
            }
        }

        private static void Clear()
        {
            registrador_a = 0;
            registrador_b = 0;
            flag_transbordo = false;
            flag_negativo = false;
            flag_zero = false;
        }
        private static void Soma()
        {  
           int output = registrador_a + registrador_b;
           ClassificarFlag(output);
           registrador_a = (short) output;   
        }

        private static void Subtrair()
        {
            var output = registrador_a - registrador_b;
            ClassificarFlag(output);
            registrador_a = (short)output;
        }

        private static void Incremento()
        {
            if(reg_op == 'A')
            {
                var output = registrador_a += 1;
                ClassificarFlag(output);
                registrador_a = output;
            }
            else
            {
                var output = registrador_b += 1;
                ClassificarFlag(output);
                registrador_a = output;
            }
        }

        private static void RetornarFlags()
        {
            Console.WriteLine("Registrador de Flags");
            Console.WriteLine("");
            Console.WriteLine($"Transbordo (Overflow): {flag_transbordo}");
            Console.WriteLine($"Negativo (Negative): {flag_negativo}");
            Console.WriteLine($"Zero (Zero): {flag_zero}");
            Console.WriteLine("");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadLine();
        }

        private static void Halt()
        {
            Console.WriteLine("");
            Console.WriteLine("HALT");
            run_bit = false;
        }

        private static void ClassificarFlag(int valor)
        {   
            if(valor == 0)
            {
                flag_zero = true;
            }
            else
            {
                flag_zero = false;
            }

            if(valor < 0)
            {
                flag_negativo = true;
            }
            else
            {
                flag_negativo = false;
            }

            try
            {
                Convert.ToInt16(valor);
                flag_transbordo = false;
            }
            catch (OverflowException)
            {
                flag_transbordo = true;
            }
        }
    }
}
