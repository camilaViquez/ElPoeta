using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections;



namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        Random rnd = new Random();

        string path = "";
        int dos = 0;
        int tres = 0;
        int cuatro = 0;
        int cinco = 0;
        int dosM = 0;
        int tresM = 0;
        int cuatroM = 0;
        int cincoM = 0;
        int cantidadPalabrasMeta = 0;
        int numIndividuos = 0;
        
        List<string> datosMeta = new List<string>();
        List<string> basedatos = new List<string>();
        List<string> basedatosMeta = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivos txt(*.txt)|*.txt";
            open.Title = "Archivo txt";

            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileName;
                //Console.WriteLine(path);
            }
            open.Dispose();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //carga Inicial de datos
        private void button1_Click(object sender, EventArgs e)
        {
            numIndividuos = Int32.Parse(textBoxIndi.Text);
            int numPoblaciones = Int32.Parse(textBoxPobla.Text);
            baseDatosMeta();
            baseDatos(numIndividuos, numPoblaciones);
            
            //Console.WriteLine("sale");
            //Poema Meta

        }

        private void baseDatosMeta()
        {
            string datosM = path;
            //string datosM = @"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\meta.txt";
            List<string> datosMetaInterfaz = new List<string>();
            datosMetaInterfaz = lecturaArchivo(datosM);
            foreach (string a in datosMetaInterfaz)
            {
                listBox5.Items.Add(a);
            }

            datosMeta = lecturaArchivoParse(datosM);

            //GenerarDiccionarios de meta 

            List<string> twoGramsM = new List<string>();
            List<string> trheeGramsM = new List<string>();
            List<string> fourGramsM = new List<string>();
            List<string> fiveGramsM = new List<string>();
            twoGramsM = GenerarNgramsM(2, datosMeta);
            trheeGramsM = GenerarNgramsM(3, datosMeta);
            fourGramsM = GenerarNgramsM(4, datosMeta);
            fiveGramsM = GenerarNgramsM(5, datosMeta);

            //Escribir base datos en txt
            System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\baseDatosMeta.txt", basedatosMeta);

            
        }
        private void baseDatos(int numIndividuos, int numPoblaciones)
        {
            //Set datos
            List<string> setDatos = new List<string>();
            string datos = @"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poems.txt";
            setDatos = lecturaArchivoParse(datos);
            //GenerarGramas de setdatos 


            List<string> twoGrams = new List<string>();
            List<string> trheeGrams = new List<string>();
            List<string> fourGrams = new List<string>();
            List<string> fiveGrams = new List<string>();

            twoGrams = GenerarNgrams(2, setDatos);
            trheeGrams = GenerarNgrams(3, setDatos);
            fourGrams = GenerarNgrams(4, setDatos);
            fiveGrams = GenerarNgrams(5, setDatos);

            cantidadPalabrasMeta = cantidadPalabras(datosMeta);
            //Console.WriteLine("cant palabras meta" + cantidadPalabrasMeta);
            generarPoblacionInicial(basedatos, basedatosMeta, cantidadPalabrasMeta, numIndividuos, numPoblaciones);
            //Escribir base datos en txt
            System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\baseDatos.txt", basedatos);

            


        }

        private List<string> lecturaArchivoParse(string ruta)
        {
            List<string> text = new List<string>();
            using (StreamReader sr = new StreamReader(ruta))
            {
                string linea;
                string[] a;
                while ((linea = sr.ReadLine()) != null)
                {
                    a = linea.Split(' ');
                    foreach (string b in a)
                    {
                        text.Add(b);
                    }
                }
            }
            return text;
        }
        private List<string> lecturaArchivo(string ruta)
        {
            List<string> text = new List<string>();
            using (StreamReader sr = new StreamReader(ruta))
            {
                string linea;
                string[] a;
                while ((linea = sr.ReadLine()) != null)
                {
                    a = linea.Split('\n');
                    foreach (string b in a)
                    {
                        text.Add(b);
                    }
                }
            }
            int numPalabras = cantidadPalabras(text);
            return text;
        }

        private int cantidadPalabras(List<string> texto)
        {
            int cont = 0;
            foreach (string palabra in texto)
            {
                cont++;
            }
            return cont;
        }
        //genera las listas con los grams 

        private List<string> GenerarNgrams(int num, List<string> texto)
        {
            List<string> ngramList = new List<string>();
            for (int k = 0; k < (texto.Count - num + 1); k++)
            {
                String s = "";
                int inicio = k;
                int fin = k + num;
                for (int j = inicio; j < fin; j++)
                {
                    s = s + " " + texto[j];

                }
                basedatos.Add(s);

            }
            if (num == 2)
            {
                dos = basedatos.Count() + ngramList.Count() - 1;
            }
            else if (num == 3)
            {
                tres = basedatos.Count() + ngramList.Count() - 1;
            }
            else if (num == 4)
            {
                cuatro = basedatos.Count() + ngramList.Count() - 1;
            }
            else if (num == 5)
            {
                cinco = basedatos.Count() + ngramList.Count() - 1;
                //Console.WriteLine("cinco" + cinco);
            }
            return basedatos;
        }
        private List<string> GenerarNgramsM(int num, List<string> texto)
        {
            List<string> ngramListM = new List<string>();
            for (int k = 0; k < (texto.Count - num + 1); k++)
            {
                String s = "";
                int inicio = k;
                int fin = k + num;
                for (int j = inicio; j < fin; j++)
                {
                    s = s + " " + texto[j];
                }
                basedatosMeta.Add(s);

            }
            if (num == 2)
            {
                dosM = basedatosMeta.Count() + ngramListM.Count() - 1;
            }

            else if (num == 3)
            {
                tresM = basedatosMeta.Count() + ngramListM.Count() - 1;
            }
            else if (num == 4)
            {
                cuatroM = basedatosMeta.Count() + ngramListM.Count() - 1;
            }
            else if (num == 5)
            {
                cincoM = basedatosMeta.Count() + ngramListM.Count() - 1;
            }
            return ngramListM;
        }
        private Dictionary<string, int> CrearHistograma(List<string> union, List<string> texto)
        {
            Dictionary<string, int> histograma = new Dictionary<string, int>();
            foreach (string palabra in union)
            {                
                histograma.Add(palabra,0);
            }
            List<string>separada = separar(texto);
            foreach (string g in separada)
            {
                if (histograma.ContainsKey(g))
                {
                    histograma[g] = histograma[g] + 1;
                }
             
            }
            return histograma;
        }

        private List<string> generarPoblacionInicial(List<string> basedatos, List<string> basedatosMeta, int palabrasMeta, int numIndividuos, int numPoblaciones)
        {
            int cont = 0;
            List<string> poblacion = new List<string>();

            while (cont < numPoblaciones)
            {
                poblacion = individuos(basedatos, basedatosMeta, palabrasMeta, numIndividuos);
                System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poblacion" + cont + ".txt", poblacion);
                cont++;
            }
            cargarMejores();
            

            return poblacion;

        }
        private void cargarMejores()
        {
            string datosM = @"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poblacion0.txt";
            List<string> datos = new List<string>();
            datos = lecturaArchivo(datosM);
            foreach (string a in datos)
            {
                listBox1.Items.Add(a);
            }

            string datosM1 = @"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poblacion1.txt";
            List<string> datos1 = new List<string>();
            datos1 = lecturaArchivo(datosM1);
            foreach (string a in datos1)
            {
                listBox2.Items.Add(a);
            }

            string datosM2 = @"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poblacion2.txt";
            List<string> datos2 = new List<string>();
            datos2 = lecturaArchivo(datosM2);
            foreach (string a in datos2)
            {
                listBox3.Items.Add(a);
            }

        }
        
        private List<string> conjuntoPalabras(List<string> poblacion)
        {
            
            List<string> poblacionSeparada = new List<string>();
            List<string> unionPalabras = new List<string>();
            poblacionSeparada = separar(poblacion);

            foreach (string parte in poblacionSeparada)
            {            
                if (!datosMeta.Contains(parte) && !unionPalabras.Contains(parte))
                {
                    unionPalabras.Add(parte);
                }
            }
            return unionPalabras;
        }
        private List<string> separar(List<string> poblacion)
        {

            List<string> text = new List<string>();
            string[] a;
            for (int i = 0; i < poblacion.Count(); i++)
            {
                
                a = poblacion[i].Split(new[] { " " }, StringSplitOptions.None);
                int largo = a.Count();
                foreach (string palabra in a)
                {
                    if(palabra != "")
                    {
                        text.Add(palabra);
                    }
                    
                }
            }
            return text;
        }
        private List<string> individuos(List<string> basedatos, List<string> basedatosMeta, int palabrasMeta, int numIndividuos)
        {
            //individuos por poblacion
            List<string> unionPalabras1 = new List<string>();
            List<string> poema1 = new List<string>();
            List<string> poema2 = new List<string>();
            List<string> poblacion = new List<string>();
            List<string> cruce = new List<string>();
            List<string> listaIndividuosT = new List<string>();
            List<string> unionPalabras2 = new List<string>();
            int distancia = 0;
            int contDist = 0;
            Dictionary<string, int> histogramaIndividuo = new Dictionary<string, int>();
            Dictionary<string, int> histogramaMeta = new Dictionary<string, int>();
            int contador = 0;
            List<int> distancias = new List<int>();
            while (contador < numIndividuos)
            {
                //listaIndividuos = generarIndividuoValido(palabrasMeta);
                poema1 = generarIndividuoValido(palabrasMeta);
                poema2 = generarIndividuoValido(palabrasMeta);
                cruce = cruzarIndividuos(poema1, poema2);

                unionPalabras1 = conjuntoPalabras(cruce);
                
                histogramaIndividuo = CrearHistograma(unionPalabras1, cruce);
                histogramaMeta = CrearHistograma(unionPalabras1, basedatosMeta);
                distancia = calcularDistancia(histogramaIndividuo, histogramaMeta);

                
                
                contDist++;
                contador++;
                for(int i=0; i < distancias.LongCount(); i++)
                {
                    //Console.WriteLine("distancias "+distancias[i]);
                }
                
            }
            
            foreach(string h in cruce)
            {
                //poblacion.Add(h);
            }
            //siguientesPoblaciones(poblacion);
            return cruce;
        }
        

        private void siguientesPoblaciones(List<string> poblacion)
        {
           
            int cont = 0;
            while (cont < poblacion.Count())
            {
                List<string> conjuntoIndividuos = new List<string>();
                conjuntoIndividuos = individuos(basedatos, basedatosMeta, cantidadPalabrasMeta, numIndividuos);
                System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\poblacion" + cont + ".txt", conjuntoIndividuos);
                cont++;
            }
        }
        private List<string> cruzarIndividuos(List<string> poema1, List<string> poema2)

        {
            string a = " ";
            foreach (string b in poema1)
            {
                a +=" " +b;     
            }
            Console.WriteLine("poema " + a);
            foreach (string c in poema2)
            {
                //Console.WriteLine("poema2 " + c);
            }
            List<string> individuoCruces = new List<string>();
            string nuevoValor = " ";
            int cantidadActual = cantidadPalabras(individuoCruces);
            while(cantidadActual < cantidadPalabrasMeta-1)
            {
                int num1 = rnd.Next(0, poema1.Count());
                int num2 = rnd.Next(0, poema2.Count());
                string valor1 = poema1[num1];
                string valor2 = poema2[num2];
                nuevoValor = valor1 +" "+valor2;
                individuoCruces.Add(nuevoValor);
                cantidadActual = cantidadPalabras(individuoCruces);
            }
            string faltante = mutar();
            individuoCruces.Add(faltante);
            return individuoCruces;
        }
        private string mutar()
        {
            string mutacion = " ";
            int num1 = rnd.Next(0, cantidadPalabrasMeta);
            mutacion = basedatosMeta[num1];
            return mutacion;
        }
        private List<string> generarIndividuoValido(int palabrasMeta)
        {
            List<string> listaIndividuo = new List<string>();
            int cont = 0;
            List<int> lineas = new List<int>();
            int maxPalabras = palabrasMeta;
            while (cont <= maxPalabras - 6)
            {
                int num = rnd.Next(0, maxPalabras);
                lineas.Add(num);
                int rango = rangosPalabra(num);
                cont += rango;
            }
            int indicesBD = lineas.Count() - 1;
            List<int> lineaFaltante = new List<int>();
            lineaFaltante = agregarFaltantes(maxPalabras - cont);
            for (int i = 0; i < lineaFaltante.Count(); i++)
            {
                lineas.Add(i);
            }
            cont = 0;
            while (cont <= indicesBD)
            {
                int posicion = lineas[cont];
                string palabra = basedatos[posicion];
                listaIndividuo.Add(palabra);
                cont++;
            }
            while (cont < lineas.Count())
            {
                int posicion = lineas[cont];
                string palabra = basedatosMeta[posicion];
                listaIndividuo.Add(palabra);
                cont++;
            }
            return listaIndividuo;
        }

        private List<int> agregarFaltantes(int faltante)
        {
            List<int> resul = new List<int>();
            if (faltante == 6)
            {
                int num = rnd.Next(dos + 1, tres);
                resul.Add(num);
                int num1 = rnd.Next(dos + 1, tres);
                resul.Add(num1);
                return resul;

            }
            else if (faltante == 5)
            {
                int num = rnd.Next(0, dos);
                resul.Add(num);
                int num1 = rnd.Next(dos + 1, tres);
                resul.Add(num1);
                return resul;

            }
            else if (faltante == 4)
            {
                int num1 = rnd.Next(0, dos);
                resul.Add(num1);
                int num2 = rnd.Next(0, dos);
                resul.Add(num2);
                return resul;
            }
            else if (faltante == 3)
            {
                int num1 = rnd.Next(dos + 1, tres);
                resul.Add(num1);
                return resul;
            }
            else if (faltante == 2)
            {
                int num = rnd.Next(0, dos);
                resul.Add(num);
                return resul;
            }
            return resul;

        }
        private int rangosPalabra(int actual)
        {
            if (actual <= dos)
            {
                //Console.WriteLine("actual " + actual);
                //Console.WriteLine("dos " + dos);

                return 2;
            }
            else if (actual <= tres)
            {
                //Console.WriteLine("actual " + actual);
                //Console.WriteLine("tres " + tres);
                return 3;

            }
            else if (actual <= cuatro)
            {
                //Console.WriteLine("actual " + actual);
                //Console.WriteLine("cuatro " + cuatro);
                return 4;
            }
            else if (actual <= cinco)
            {
                //Console.WriteLine("actual " + actual);
                //Console.WriteLine("cinco " + cinco);
                return 5;
            }
            return 0;

        }

        private int calcularDistancia(Dictionary<string, int> histogramaIndividuo, Dictionary<string, int> histogramaMeta)
        {
            distanciaManhatthan(histogramaIndividuo, histogramaMeta);
            Chebyshev(histogramaIndividuo, histogramaMeta);
            return 0;

        }

        private int distanciaManhatthan(Dictionary<string, int> histogramaIndividuo, Dictionary<string, int> histogramaMeta)
        {
            List<int> listaP = new List<int>();
            List<int> listaQ = new List<int>();
            int fitness = 30;
            
            listaP = obtenerValores(histogramaIndividuo);
            listaQ = obtenerValores(histogramaMeta);

            int p = 0;
            int q = 0;
            int cont = 0;
            int sumatoria = 0;
            int contDist = 0;

      
            while (cont < listaP.Count)
            {

                p = listaP[cont];
                q = listaQ[cont];
                //Console.WriteLine("p " + p);
                //Console.WriteLine("q " + q);
                int resul = Math.Abs(p - q);
                //Console.WriteLine("resul " + resul);
                sumatoria += resul;
                Console.WriteLine("sumatoria " + sumatoria);
                cont++;
                string sumaS = Convert.ToString(sumatoria);
                List<string> escribir = new List<string>();
                if (sumatoria < fitness)
                {
                    escribir.Add(sumaS);
                    System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\ditanciaM" + contDist + ".txt", escribir);
                    contDist++;

                }
               

            }
            
            contDist++;

            return sumatoria;
        }

        private int campana(Dictionary<string, int> histogramaIndividuo, Dictionary<string, int> histogramaMeta)
        {
            List<int> listaP = new List<int>();
            List<int> listaQ = new List<int>();
            int varianza = 30;
            listaP = obtenerValores(histogramaIndividuo);
            
            listaQ = obtenerValores(histogramaMeta);
            
            int p = 0;
            int q = 0;
            int cont = 0;
            int sumatoria = 0;
            int contDist = 0;
            int maxRango = 0;
            int minRango = 0;
            int distPrometedora = 0;

            while (cont < listaP.Count)
            {

                p = listaP[cont];
                q = listaQ[cont];
                maxRango = q + varianza;
                minRango = q + varianza;
                
                if(minRango < listaP[cont+1] && listaP[cont + 1]<maxRango)
                {
                    distPrometedora += listaP[cont + 1];

                }
                cont++;

                string sumaS = Convert.ToString(distPrometedora);
                List<string> escribir = new List<string>();
                escribir.Add(sumaS);
                System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\ditanciaCampana" + contDist + ".txt", escribir);
                contDist++;

            }

            contDist++;

            return distPrometedora;
        }

        private int Chebyshev(Dictionary<string, int> histogramaIndividuo, Dictionary<string, int> histogramaMeta)
        {

            int p = 0;
            int q = 0;
            int cont = 0;
            int result = 0;
            int resta = 0;
            int fitness = 30;
            List<int> restas = new List<int>();
            int contDist = 0;
            while (cont < histogramaIndividuo.Count())
            {
                p = histogramaIndividuo[cont];
                q = histogramaMeta[cont];
                resta = p - q;
                if (resta < fitness)
                {
                    restas.Add(resta);
                }
                string restaS = Convert.ToString(resta);
                List<string> escribir = new List<string>();
                escribir.Add(restaS);
                System.IO.File.WriteAllLines(@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\ditanciaC" + contDist + ".txt", escribir);
                contDist++;
                cont++;
            }
            result = restas.Max();
            return result;
        }

        private List<int> obtenerValores(Dictionary<string, int> histograma)
        {
            List<int> valores = new List<int>();
            foreach (KeyValuePair<string, int> result in histograma)
            {
                //Console.WriteLine(string.Format("Key-{0}:Value-{1}", result.Key, result.Value));
                valores.Add(result.Value);
                
            }
            for(int i=0; i < valores.Count(); i++)
            {
                Console.WriteLine("valor "+valores[i]);
            }
            return valores;
        }














        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        //Manhatthan
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
//@"C:\Users\camil\OneDrive\Documents\TEC\Análisis\ElPoeta\flores.txt"
//int gg = Int32.Parse(g);
//foreach (KeyValuePair<string, int> result in claveValor)
 //           {
   //             Console.WriteLine(string.Format("Key-{0}:Value-{1}", result.Key, result.Value));
     // 
     
/*
     foreach (string a in poblacion)
            {
                Console.WriteLine("poblacion " + a);
            }
            */
              