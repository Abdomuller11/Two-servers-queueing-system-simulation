using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueSimulation
{

    public class TwoServers
    {
        const int able_pri = 1;
        const int baker_pri = 2;   //stp     
        public TwoServers()
        {

        }

        public struct Performance_measure
        {
            public int total_able_service_time;
            public int total_baker_service_time;
            public int timeInQ;
            public int peopleInQ;
            public int avg_waiting_t;
            public int prob_of_wait;
            public int avgServiceTimeable;
            public int avgServiceTimebaker;
            public int prob_idle_able ;
            public int prob_idle_baker;
        }
        Performance_measure[] performance = new Performance_measure[2];
        public Performance_measure[] Performance
        {
            get { return performance; }
            set { performance = value; }
        }


        public struct Customer_Details
        {
            public int customer_num;
            public int r_interarrival;
            public int interarrival;
            public int arrive_time;
            public int r_serv;
            public string server_selected;
            public int able_service_time;
            public int able_service_begin_time;
            public int able_service_end_time;
            public int baker_service_time;
            public int baker_service_begin_time;
            public int baker_service_end_time;

            public int Time_in_q;
            public int people_in_q;
        }
        Customer_Details[] customee = new Customer_Details[400];    //14 attrebutes
                                                                    //int[] arr_who_entered = new int[1];            //flag                 //دخلت انهي      //   -->Able0    -->Baker1

        public Customer_Details[] Customee
        {
            get { return customee; }
            set { customee = value; }
        }
        //public int[] Arr_who_entered
        //{
        //    get { return arr_who_entered; }
        //    set { arr_who_entered = value; }
        //}



        //implementations

        public void fill_customer_no()
        {
            for (int i = 0; i < 100; i++)
            {
                Customee[i].customer_num = (i + 1);
            }
        }

        public void R_interarrival_time()
        {
            Random r = new Random();
            Customee[0].r_interarrival = 0;
            for (int i = 1; i < 100; i++)
            {
                Customee[i].r_interarrival = r.Next(1, 100);
            }
        }

        public void Interarrival_time()//if x=-1 then calc on R_interarrival_time() else parameterized
        {
            Customee[0].interarrival = 0;
            for (int j = 1; j < 100; j++)
            {
                //zero case
                if (Customee[j].r_interarrival >= 1 && Customee[j].r_interarrival <= 25)
                {
                    Customee[j].interarrival = 1;
                }
                else if (Customee[j].r_interarrival >= 26 && Customee[j].r_interarrival <= 65)
                {
                    Customee[j].interarrival = 2;
                }
                else if (Customee[j].r_interarrival >= 66 && Customee[j].r_interarrival <= 85)
                {
                    Customee[j].interarrival = 3;
                }
                else
                {
                    Customee[j].interarrival = 4;
                }
            }
        }
        public void Arrive_time()
        {
            Customee[0].arrive_time = 0;
            for (int i = 1; i < 100; i++)
            {
                Customee[i].arrive_time = Customee[i - 1].arrive_time + Customee[i].interarrival;
            }
        }

        public void R_service()
        {
            Random r2 = new Random();
            for (int i = 0; i < 100; i++)
            {
                Customee[i].r_serv = r2.Next(1, 100);
            }
        }

        public bool selection_method(int index, int x1, int x2, int x = 1)//(true  Able)    (false   Baker)             //SelectionMethod.HighestPriority
        {
            //1--> priority       2-->random            3-->least utilization
            if (x == 1)//Priority
            {
                if (able_pri < baker_pri)      //id=priority      #stp
                {
                    //by default
                    //اختارت ابيل
                    Customee[index].server_selected = "able";
                    return true;
                }
                else
                {//اختارت باكير
                    Customee[index].server_selected = "baker";
                    return false;
                }
            }
            else if (x == 2)
            {
                Random r = new Random();

                int val = r.Next(0, 1);
                if (val == 0)
                {
                    Customee[index].server_selected = "able";
                    return true;
                }
                else
                {
                    Customee[index].server_selected = "baker";
                    return false;
                }
            }
            else
            {
                return LeastUtil(x1, x2, index);
            }
        }
        public bool LeastUtil(int x1, int x2, int index)//x1 total service able ,,x2 baker
        {
            if (x1 < x2)
            {// able اشتغل اقل
                Customee[index].server_selected = "able";
                return true;
            }
            else
            {
                Customee[index].server_selected = "baker";
                return false;
            }
        }

        public void serviceTime(int x, int index)
        {
            if (x == 0)//belongs to Able sys
            {
                if (Customee[index].r_serv >= 1 && Customee[index].r_serv <= 30)
                {
                    Customee[index].able_service_time = 2;

                }
                else if (Customee[index].r_serv >= 31 && Customee[index].r_serv <= 58)
                {
                    Customee[index].able_service_time = 3;
                }
                else if (Customee[index].r_serv >= 59 && Customee[index].r_serv <= 83)
                {
                    Customee[index].able_service_time = 4;

                }
                else
                {
                    Customee[index].able_service_time = 5;

                }
            }
            else //belongs to Baker sys
            {
                if (Customee[index].r_serv >= 1 && Customee[index].r_serv <= 35)
                {
                    Customee[index].baker_service_time = 3;

                }
                else if (Customee[index].r_serv >= 36 && Customee[index].r_serv <= 60)
                {
                    Customee[index].baker_service_time = 4;

                }
                else if (Customee[index].r_serv >= 61 && Customee[index].r_serv <= 80)
                {
                    Customee[index].baker_service_time = 5;

                }
                else
                {
                    Customee[index].baker_service_time = 6;
                }
            }
        }





        public void customers(int no, int x = 1)//no customers     x slection method
        {
            int max0 = 0, max1 = 0, low;
            int[] last_num_notEqualZero0 = new int[100];
            int[] last_num_notEqualZero1 = new int[100];
            int total_able_service_time_term = 0, total_baker_service_time_term = 0;
            //zero case harded code


            Customee[0].Time_in_q = 0;
            Customee[0].people_in_q = 0;
            if (selection_method(0, 0, 0, x))
            {
                //able
                serviceTime(0, 0);
                Customee[0].able_service_begin_time = Customee[0].arrive_time;
                Customee[0].able_service_end_time = Customee[0].able_service_begin_time + Customee[0].able_service_time;
                total_able_service_time_term = Customee[0].able_service_time;
            }
            else
            {
                //baker
                serviceTime(0, 0);
                Customee[0].baker_service_begin_time = Customee[0].arrive_time;
                Customee[0].baker_service_end_time = Customee[0].baker_service_begin_time + Customee[0].baker_service_time;
                total_baker_service_time_term = Customee[0].baker_service_time;
            }





            for (int k = 1; k < no; k++)
            {
                for (int j = 0; j < Customee[k].customer_num; j++)
                {
                    if (Customee[j].able_service_end_time > max0)
                    {
                        max0 = Customee[j].able_service_end_time;
                    }
                    if (Customee[j].baker_service_end_time > max1)
                    {
                        max1 = Customee[j].baker_service_end_time;
                    }

                }
                low = Math.Min(max0, max1);
                if (Customee[k].arrive_time < max0 && Customee[k].arrive_time < max1)
                {
                    //Customee[k].people_in_q += 1;
                    Customee[k].people_in_q = Customee[k - 1].people_in_q + 1;
                    Customee[k].Time_in_q = low - Customee[k].arrive_time;
                }

                if (Customee[k].arrive_time >= max0 && Customee[k].arrive_time < max1)
                {
                    //able
                    if (Customee[k - 1].people_in_q == 0) { Customee[k].people_in_q = 0; }
                    else { Customee[k].people_in_q = Customee[k - 1].people_in_q - 1; }

                    Customee[k].able_service_begin_time = Customee[k].arrive_time;



                    serviceTime(0, k);
                    Customee[k].able_service_end_time = Customee[k].able_service_begin_time + Customee[k].able_service_time;
                    Customee[k].server_selected = "able";

                    total_able_service_time_term += Customee[k].able_service_time;

                }
                else if (Customee[k].arrive_time >= max1 && Customee[k].arrive_time < max0)
                {
                    //baker
                    if (Customee[k - 1].people_in_q == 0) { Customee[k].people_in_q = 0; }
                    else { Customee[k].people_in_q = Customee[k - 1].people_in_q - 1; }

                    Customee[k].baker_service_begin_time = Customee[k].arrive_time;

                    serviceTime(1, k);
                    Customee[k].baker_service_end_time = Customee[k].baker_service_begin_time + Customee[k].baker_service_time;
                    Customee[k].server_selected = "baker";
                    total_baker_service_time_term += Customee[k].baker_service_time;
                }
                else if (Customee[k].arrive_time >= max0 && Customee[k].arrive_time >= max1)
                {
                    if (selection_method(k, total_able_service_time_term, total_baker_service_time_term, x))
                    {
                        //able
                        if (Customee[k - 1].people_in_q == 0) { Customee[k].people_in_q = 0; }
                        else { Customee[k].people_in_q = Customee[k - 1].people_in_q - 1; }

                        Customee[k].able_service_begin_time = Customee[k].arrive_time;

                        serviceTime(0, k);
                        Customee[k].able_service_end_time = Customee[k].able_service_begin_time + Customee[k].able_service_time;
                        Customee[k].server_selected = "able";
                        total_able_service_time_term += Customee[k].able_service_time;
                    }
                    else
                    {
                        //baker
                        if (Customee[k - 1].people_in_q == 0) { Customee[k].people_in_q = 0; }
                        else { Customee[k].people_in_q = Customee[k - 1].people_in_q - 1; }

                        Customee[k].baker_service_begin_time = Customee[k].arrive_time;

                        serviceTime(1, k);
                        Customee[k].baker_service_end_time = Customee[k].baker_service_begin_time + Customee[k].baker_service_time;
                        Customee[k].server_selected = "baker";
                        total_baker_service_time_term += Customee[k].baker_service_time;
                    }
                }
                else
                {//Customee[k - 1].baker_service_end_time < Customee[k - 1].able_service_end_time
                 //for (int q = 0; q < Customee[k].customer_num; q++)
                 //{
                 //    last_num_notEqualZero0[q] = Customee[q].able_service_end_time;
                 //    last_num_notEqualZero1[q] = Customee[q].baker_service_end_time;
                 //}
                 //for (int g = 0; g < last_num_notEqualZero0.Length; g++)
                 //{
                 //    if (last_num_notEqualZero0[g] > res0)
                 //    {
                 //        res0 = last_num_notEqualZero0[g];
                 //    }
                 //    if (last_num_notEqualZero1[g] > res1)
                 //    {
                 //        res1 = last_num_notEqualZero1[g];
                 //    }
                 //}
                    if (max0 < max1)
                    {
                        //able
                        Customee[k].able_service_begin_time = max0;
                        serviceTime(0, k);
                        Customee[k].able_service_end_time = Customee[k].able_service_begin_time + Customee[k].able_service_time;
                        Customee[k].server_selected = "able";
                        total_able_service_time_term += Customee[k].able_service_time;
                    }
                    else if (max1 < max0)
                    {
                        //baker
                        Customee[k].baker_service_begin_time = max1;
                        serviceTime(1, k);
                        Customee[k].baker_service_end_time = Customee[k].baker_service_begin_time + Customee[k].baker_service_time;
                        Customee[k].server_selected = "baker";
                        total_baker_service_time_term += Customee[k].baker_service_time;
                    }
                    else
                    {
                        if (selection_method(k, total_able_service_time_term, total_baker_service_time_term, x))
                        {
                            //able
                            Customee[k].able_service_begin_time = max0;
                            serviceTime(0, k);
                            Customee[k].able_service_end_time = Customee[k].able_service_begin_time + Customee[k].able_service_time;
                            Customee[k].server_selected = "able";
                            total_able_service_time_term += Customee[k].able_service_time;
                        }
                        else
                        {
                            //baker
                            Customee[k].baker_service_begin_time = max1;
                            serviceTime(1, k);
                            Customee[k].baker_service_end_time = Customee[k].baker_service_begin_time + Customee[k].baker_service_time;
                            Customee[k].server_selected = "baker";
                            total_baker_service_time_term += Customee[k].baker_service_time;
                        }
                    }

                }

            }
            //performance measurements




        }


        public void main_program(int stop = 0, int no = 0, int x = 1) // StoppingCriteria          //no. customers  or no==0  simulated time                //selection method
        {
            fill_customer_no();
            R_interarrival_time();
            Interarrival_time();
            Arrive_time();
            R_service();
            customers(no, x);


            ////if(stop==0 && no != 0)  //if stop ==0 , no!=0 then (stopping criteria)=no customers
            ////{

            /// }

            //performance measurements
            int total_able_service_time = 0, total_baker_service_time = 0, timeInQ = 0, peopleInQ = 0, avg_waiting_t = 0, prob_of_wait = 0, avgServiceTimeable = 0, avgServiceTimebaker = 0;
            int prob_idle_able = 0, prob_idle_baker = 0, totalServiceTime = 0;
            for (int y = 0; y < no; y++)
            {

                total_able_service_time += Customee[y].able_service_time;
                total_baker_service_time += Customee[y].baker_service_time;
                timeInQ += Customee[y].Time_in_q;
                if (Customee[y].people_in_q > peopleInQ)
                {
                    peopleInQ = Customee[y].people_in_q;
                }
                //12
                //11
                //12
                //13
            }
            //Performance==utilization

            Performance[0].total_able_service_time = total_able_service_time;
            Performance[1].total_baker_service_time = total_baker_service_time;
            totalServiceTime = Performance[0].total_able_service_time + Performance[1].total_baker_service_time;
            Performance[0].peopleInQ = peopleInQ;
            Performance[0].timeInQ = timeInQ;
            
           
            
            avg_waiting_t = Performance[0].timeInQ / no;
            prob_of_wait = peopleInQ / no;
            avgServiceTimeable = total_able_service_time;
            avgServiceTimebaker = total_baker_service_time;
            prob_idle_able = totalServiceTime - total_able_service_time;
            prob_idle_baker = totalServiceTime - total_baker_service_time;

            Performance[0].avg_waiting_t = avg_waiting_t;
            Performance[0].prob_of_wait = prob_of_wait;
            Performance[0].avgServiceTimeable = avgServiceTimeable;
            Performance[0].avgServiceTimebaker = avgServiceTimebaker;
            Performance[0].prob_idle_able = prob_idle_able;
            Performance[0].prob_idle_baker = prob_idle_baker;

        }

       
    }




    













}
