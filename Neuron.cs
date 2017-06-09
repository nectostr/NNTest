using System;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    public class Neuron
    {
        const int amInputs = 2;

        public double[] inputs = new double[amInputs];
        public double[] synapths= new double[amInputs];
        public double output;
        //public double error;
        private double ownWeight = 0.3;
        

        public void thnk()
        {
            //функция суммирования
            output = 0;
            for(int i = 0; i < inputs.Length; i++)
            {
                output += inputs[i] * synapths[i];
            }
            output += 1 * ownWeight;
            /// функция активации
            output = 1 / (1 + Math.Pow(Math.E, output));

        }

        public void makeErrCh(double error)
        {
            for (int i = 0; i < synapths.Length; i++)
            {
                synapths[i] = synapths[i]
                    * (-Math.Pow(Math.E, synapths[i]) / (Math.Pow((Math.Pow(Math.E, synapths[i]) + 1), 2)))//производная активации
                    * error  //ошибка
                    * 0.01; // скорость обучения

            }
        }
    }
}
