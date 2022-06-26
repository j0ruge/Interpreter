using System;

namespace Interpreter
{
    class Program
    {

        static int program_counter; // contador de programa contém endereço da próxima instruction
        static int accumulator; // o acumulador, um registrador para efetuar aritmética
        static int instruction; // um registrador para conter a instrução corrente
        static int instr_type; // o tipo da instrução (opcode)
        static int data_loc; // o endereço dos dados, ou –1 se nenhum
        static int data; // mantém o operando corrente
        static bool run_bit = false; // um bit que pode ser desligado para parar a máquina
        const int CLR = 1000;// <-- seta o valor no accumulator para 0
        const int ADDI = 1005;// <-- adiciona o valor x no accumulator
        const int ADDM = 1010;// <-- adiciona o valor da memória y no accumulator
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
            if (opcode == ADDI)
            {
                return program_counter;
            }
            if (opcode == ADDM)
            {
                return memory[program_counter];
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
            if (instr_type == ADDI)
            {
                accumulator = accumulator + data;
                Console.WriteLine(accumulator);
            }
            if (instr_type == ADDM)
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
			int[] m2 = { 2, -5, 15, CLR, // o "programa" inicia aqui
				ADDI, 12, ADDI, 7, ADDM, 0, ADDM, 1, CLR, HALT };
			Console.WriteLine("Imagem de memória 1: ");
			interpret(m2, 3);// start at CLR

			int[] m3 = { 1, 3, 5, CLR, // o "programa" inicia aqui
				ADDI, 7, ADDM, 2, CLR, ADDM, 0, ADDM, 1, CLR, HALT };
			Console.WriteLine("Imagem de memória 2: ");
			interpret(m3, 3); // start at CLR

			int[] m4 = { 13, -5, 7, 8, CLR,//o "programa" inicia aqui
				         ADDM, 1, 3, ADDM, 1, CLR, HALT
            };
			Console.WriteLine("Imagem de memória 3: ");
			interpret(m4, 4);			
        }
    }
}
