using System;

namespace VirtualCPU
{
    class Program
    {
        static int menuOp = -1;
        
        static string instructorRegister = "0000";
        static bool run_bit = true; //um bit que pode ser desligado para parar a máquina

        #region Registers
        static int programCounter = 0;
        static string registrador_A = "0000";
        static string registrador_B = "0000";
        #endregion

        #region OP Code
        const string ADD = "0001";
        const string SUB = "0010";
        const string AND = "0011";
        const string OR  = "0100";
        const string XOR = "0101";
        const string INCREMENT_A = "0110";
        const string CLEAR = "0111";
        const string HALT = "1111";
        #endregion

        #region Flags
        static bool flag_transbordo = false;
        static bool flag_negativo = false;
        static bool flag_zero = false;
        #endregion

        //4 Bits Instructions

        //ADD Reg_A Reg_B
        static string[] M1 = 
        {
            "0001",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1000",
            "0011"
        };

        //SUB Reg_A Reg_B
        static string[] M2 =
        {
            "0010",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1111",
            "1000"
        };

        //AND Reg_A Reg_B
        static string[] M3 =
        {
            "0011",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //OR Reg_A Reg_B
        static string[] M4 =
        {
            "0100",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //XOR Reg_A Reg_B
        static string[] M5 =
        {
            "0101",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //INCREMENT_A
        static string[] M6 =
        {
            "0110",
            "0110",
            "0111",
            "1111",
            "0000",
            "0000",
            "1111",
            "0000"
        };

        //INCREMENT_A CLEAR
        static string[] M7 =
        {
            "0110",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1011",
            "0000"
        };

        static string[] selectedMemorySetup = M1;

        static void Main()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Menu Principal da Maquina Virtual");
            Console.WriteLine("");
            Console.WriteLine("  1. M1 => ADD Reg_A Reg_B");
            Console.WriteLine("  2. M2 => SUB Reg_A Reg_B");
            Console.WriteLine("  3. M3 => AND Reg_A Reg_B");
            Console.WriteLine("  4. M4 => OR Reg_A Reg_B");
            Console.WriteLine("  5. M5 => XOR Reg_A Reg_B");
            Console.WriteLine("  6. M6 => INCREMENT_A");
            Console.WriteLine("  7. M7 => INCREMENT_A CLEAR");
            Console.WriteLine("");
            Console.WriteLine("Escolha um setup de memória =>");

            var retorno = Console.ReadLine();

            if (retorno.Length == 0 || int.Parse(retorno) < 1 || int.Parse(retorno) > 8)
                Main();
            else
                menuOp = int.Parse(retorno);

            switch (menuOp)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("M1 => ADD Reg_A Reg_B");
                    Console.WriteLine();
                    selectedMemorySetup = M1;
                    Programa();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("M2 => SUB Reg_A Reg_B");
                    Console.WriteLine();
                    selectedMemorySetup = M2;
                    Programa();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("M3 => AND Reg_A Reg_B");
                    Console.WriteLine();
                    selectedMemorySetup = M3;
                    Programa();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("M4 => OR Reg_A Reg_B");
                    Console.WriteLine();
                    selectedMemorySetup = M4;
                    Programa();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("M5 => XOR Reg_A Reg_B");
                    Console.WriteLine();
                    selectedMemorySetup = M5;
                    Programa();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("M6 => INCREMENT_A");
                    Console.WriteLine();
                    selectedMemorySetup = M6;
                    Programa();
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("M7 => INCREMENT_A CLEAR");
                    Console.WriteLine();
                    selectedMemorySetup = M7;
                    Programa();
                    break;
            }           
        }

        static void Programa()
        {
            PrintMemory(selectedMemorySetup, "INICIAL");
            PrintFlags("INICIAIS");
            PrintRegisters("INICIAIS");

            Console.WriteLine("== EXECUÇÃO ==");
            Console.WriteLine();

            //INICIO CICLO
            while (run_bit)
            {
                instructorRegister = GetMemoryCell(selectedMemorySetup, programCounter);
                SelectOpCode(instructorRegister);
            }

            Console.WriteLine();
            Console.WriteLine("== FIM EXECUÇÃO ==");
            Console.WriteLine();

            PrintMemory(selectedMemorySetup, "FINAL");
            PrintFlags("FINAIS");
            PrintRegisters("FINAIS");

            Console.WriteLine();
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadLine();
        }

        static void SelectOpCode(string opCode)
        {
            switch (opCode)
            {
                case SUB:
                    Console.WriteLine("SUB A B");
                    programCounter++;
                    Sub();
                    programCounter++;
                    break;
                case ADD:
                    Console.WriteLine("ADD A B");
                    programCounter++;
                    Add();
                    programCounter++;
                    break;
                case AND:
                    Console.WriteLine("AND A B");
                    programCounter++;
                    And();
                    programCounter++;
                    break;
                case OR:
                    Console.WriteLine("OR A B");
                    programCounter++;
                    Or();
                    programCounter++;
                    break;
                case XOR:
                    Console.WriteLine("XOR A B");
                    programCounter++;
                    Xor();
                    programCounter++;
                    break;
                case INCREMENT_A:
                    Console.WriteLine("INCREMENT_A");
                    programCounter++;
                    IncrementA();
                    programCounter++;
                    break;
                case CLEAR:
                    Console.WriteLine("CLEAR");
                    programCounter++;
                    Clear();
                    break;
                case HALT:
                    Console.WriteLine("HALT");
                    Halt();
                    break;
            }
        }

        static void Add()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(selectedMemorySetup, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) + Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void Sub()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            registrador_B = GetMemoryCell(selectedMemorySetup, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) - Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4,'0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void And()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(selectedMemorySetup, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) & Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void Or()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(selectedMemorySetup, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) | Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void Xor()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(selectedMemorySetup, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) ^ Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void IncrementA()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(selectedMemorySetup, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            var resultado = Convert.ToInt16(registrador_A, 2) + 1;

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            registrador_A = registrador_A.Length > 4 ? "0000" : registrador_A;
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(selectedMemorySetup, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(selectedMemorySetup, enderecoGuardar, registrador_A);
        }

        static void Halt()
        {
            run_bit = false;
        }

        static void Clear()
        {
            ReturnOriginalState();
        }

        static void ReturnOriginalState()
        {
            registrador_A = "0000";
            registrador_B = "0000";
            instructorRegister = "0000";
            flag_transbordo = false;
            flag_negativo = false;
            flag_zero = false;
        }      

        //Utilizado para pegar a célula de memória desejada
        static string GetMemoryCell(string[] memory, int rowNumber)
        {            
            return memory[rowNumber];
        }

        //Utilizado para guardar valor a célula de memória desejada
        static void SetMemoryCell(string[] memory, int rowNumber, string valor)
        {
            memory[rowNumber] = valor;
        }

        private static void ClassificarFlag(int valor)
        {   
            //Verificar se é zero
            if (valor == 0)
            {
                flag_zero = true;
            }
            else
            {
                flag_zero = false;
            }

            //Verificar se valor é menor que zero
            if (valor < 0)
            {
                flag_negativo = true;
            }
            else
            {
                flag_negativo = false;
            }

            //Verificar se valor é maior que o permitido do registrador
            if(valor > 15)
            {
                flag_transbordo = true;
            }
            else
            {
                flag_transbordo = false;
            }
        }

        private static void PrintMemory(string[] memory, string momento)
        {
            Console.WriteLine($"== MEMÓRIA {momento} ==");
            Console.WriteLine();

            for (int i = 0; i < memory.Length; i++)
            {
                Console.WriteLine($"MEM[{i}] {memory[i]}");
            }
            Console.WriteLine();
            Console.WriteLine($"== FIM MEMÓRIA {momento} ==");
            Console.WriteLine();
        }

        private static void PrintFlags(string momento)
        {
            Console.WriteLine($"== FLAGS {momento} ==");
            Console.WriteLine();
            Console.WriteLine($"Transbordo (Overflow): {flag_transbordo}");
            Console.WriteLine($"Negativo (Negative): {flag_negativo}");
            Console.WriteLine($"Zero (Zero): {flag_zero}");
            Console.WriteLine();
            Console.WriteLine($"== FIM FLAGS {momento} ==");
            Console.WriteLine();
        }

        private static void PrintRegisters(string momento)
        {
            Console.WriteLine($"== REGISTRADORES {momento} ==");
            Console.WriteLine();
            Console.WriteLine($"REGISTRADOR_A: {registrador_A}");
            Console.WriteLine($"REGISTRADOR_B: {registrador_B}");
            Console.WriteLine($"IR: {instructorRegister}");
            Console.WriteLine();
            Console.WriteLine($"== FIM REGISTRADORES {momento} ==");
            Console.WriteLine();
        }
    }
}