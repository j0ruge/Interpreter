using System;

namespace Interpreter
{
    class Program
    {
        static int menu_op = 0;             //valor para seleção do menu
        static int code_op = 0;             //valor para seleção da operação
        static int registrador_a = 0;       
        static int registrador_b = 0;       
        static char reg_op = 'A';           //usado para recursividade das seleções de registrador

        static int program_counter; // contador de programa contém endereço da próxima instruction
        static int accumulator; // o acumulador, um registrador para efetuar aritmética
        static int instruction; // um registrador para conter a instrução corrente
        static int instr_type; // o tipo da instrução (opcode)
        static int data_loc; // o endereço dos dados, ou –1 se nenhum
        static int data; // mantém o operando corrente
        static bool run_bit = false; // um bit que pode ser desligado para parar a máquina
        const int CLR = 1000;// <-- seta o valor no accumulator para 0
        const int ADD = 000;// <-- adiciona o valor x no accumulator        
        const int HALT = 1100;// <-- instrução que desliga o processador

        public static void interpret(int[] memory, int starting_address)
        {
            /**
             * Esse procedimento interpreta programas para uma máquina simples com instruções que têm
             * um operando na memória. A máquina tem um registrador accumulator, usado para
             * aritmética. A instrução ADD soma um inteiro na memória do accumulator, por exemplo.
             * O interpretador continua funcionando até o bit de funcionamento ser desligado pela instrução HALT.
             * O estado de um processo que roda nessa máquina consiste em memória, o
             * contador de programa, bit de funcionamento e accumulator. Os parâmetros de entrada consistem 
             * na imagem da memória e no endereço inicial.
             */

            program_counter = starting_address;
            run_bit = true;
            while (run_bit)
            {
                instruction = memory[program_counter]; // busca a próxima instrução e armazena em instruction
                program_counter = program_counter + 1; // incrementa contador de programa
                instr_type = get_instr_type(instruction); // determina tipo da instrução
                data_loc = find_data(instruction, instr_type, memory); // localiza dados (–1 se nenhum)
                if (data_loc >= 0) // se data_loc é –1, não há nenhum operando
                { data = memory[data_loc]; } // busca os dados
                execute(instr_type, data); // executa instrução
            }
        }


        private static int get_instr_type(int opcode)
        {
            return opcode;
        }
        private static int find_data(int opcode, int type, int[] memory)
        {
            if (opcode == ADD)
            {
                return program_counter;
            }
            else
                return -1;
        }
        private static void execute(int instr_type, int data)
        {
            if (instr_type == CLR)
            {
                accumulator = 0;
                Console.WriteLine(accumulator);
            }
            if (instr_type == ADD)
            {
                accumulator = accumulator + data;
                Console.WriteLine(accumulator);
            }     
            if (instr_type == HALT)
            {
                run_bit = false;
            }

        }

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            while (menu_op != 8)
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
                        Operacao();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Executar ULA");
                        Console.WriteLine("Aperte qualquer tecla para continuar");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void DefinirRegistrador(char reg)
        {
            if(reg == 'A')
            {
                Console.WriteLine("Definir registrador A");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador(reg_op);
                }
                else
                {
                    registrador_a = int.Parse(retorno);
                }
                    
            }
            else
            {
                Console.WriteLine("Definir registrador B");

                var retorno = Console.ReadLine();
                if (retorno.Length == 0)
                {
                    Console.WriteLine("Valor não inserido");
                    Console.WriteLine("");
                    DefinirRegistrador(reg_op);
                }
                else
                {
                    registrador_b = int.Parse(retorno);
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

        static void Operacao()
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
                Operacao();
            else
                code_op = int.Parse(retorno);
        }
    }
}
