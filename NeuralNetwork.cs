using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

namespace ConsoleApp2
{
    public class NeuralNetwork
    {
        Random rnd = new Random();
        const int amount = 2;

        int[,] gInput = new int[4,2] { {0,0},{0,1 },{1,0 },{1,1 } };
        int[] answers = new int[4] {0,1,1,0 };
        
        

        Neuron[] hiddenNeurons = new Neuron[amount];
        Neuron[] inputNeurons = new InputNeuron[amount];
        Neuron outputNeuron;

        void init()
        {
            for (int i = 0; i < inputNeurons.Length; i++)
            {
                for (int j = 0; j < inputNeurons[i].synapths.Length; j++)
                    inputNeurons[i].synapths[j] = 1;
            }
            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                for (int j = 0; j < hiddenNeurons[i].synapths.Length; j++)
                    hiddenNeurons[i].synapths[j] = rnd.Next();
            }
            for (int j = 0; j < outputNeuron.synapths.Length; j++)
                outputNeuron.synapths[j] = rnd.Next();            
        }
        // проброс значений через сетку, оптимизировать
        double thnk(int a)
        {
            for (int i = 0; i < inputNeurons.Length; i++)
            {
                for (int j = 0; j < 2; j ++)
                    inputNeurons[i].inputs[j] = gInput[a,i];
            }
            for (int i = 0; i < inputNeurons.Length; i++)
            {
                inputNeurons[i].thnk();
            }
            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                for (int j = 0; j < hiddenNeurons[i].inputs.Length; j++)
                   hiddenNeurons[i].inputs[j] = inputNeurons[i].output;
            }
            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                hiddenNeurons[i].thnk();
            }
            for (int i = 0; i < outputNeuron.inputs.Length; i++)
                outputNeuron.inputs[i] = hiddenNeurons[i].output;
            return outputNeuron.output;
        }
        double gError;
        double[] errors = new double[2];
        // обучим сетку
        public int teachMe()
        {
            for (int p = 0; p < 4; p++)
            {
                thnk(p);
                double lErr = answers[p] - outputNeuron.output;
                gError += Math.Abs(lErr);
                outputNeuron.makeErrCh(lErr);
                double[] lErrArr = new double[2]; //очередная стремная константа заменить
                for (int i = 0; i < 3; i++) // стремная константа количества нейронов в слое, который у меня пока даже не массив
                {
                    hiddenNeurons[i].makeErrCh(outputNeuron.synapths[i]);
                }//дико стремный цикл
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        inputNeurons[i].makeErrCh(hiddenNeurons[j].synapths[i]);
                    }
                }





            }
            return 0;
        }



    }
}
