using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Flight_Assistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void findflights_Click_1(object sender, EventArgs e)
        {
            try
            {



                String cheap = "";
               // if(cheapestflight.Checked)
                {
                //    cheap = "MIN(F.flight_price),";
                }
               // else 
                {
                 //   cheap = "all";
                }


                string Query="";
             
                if (airlinecheck.Checked)
                {

                    //Query = "select A.airline_name, F.flight_no, AR.arrival_time, AP.AirportName from airline A, flights F, arrives AR, airport AP = " + "\""+ textBox1.Text + "\" and A.flight_no = F.flight_no and F.flight_no = AR.flight_no and AR.AirportName = AP.AirportName";
                    Query = "select A.airline_name, F.flight_no, AR.arrival_time, AP.AirportName from airline A, flights F, arrives AR, airport AP where A.airline_name = "+ "\""+textBox1.Text+ "\"  and A.flight_no = F.flight_no and F.flight_no = AR.flight_no and AR.AirportName = AP.AirportName";
                }

                if (departureairportcheck.Checked && !cheapestflight.Checked)
                {
                  
                    Query = "select "+cheap+"  F.flight_no, F.arrival_time, F.departure_time, F.flight_price,a.AirportName,a.AirportLoc from flights F,departs d, airport a where d.flight_no=F.flight_no AND d.AirportName=a.AirportName AND d.AirportName="+"\""+departureairport.Text+"\"";
                }
                 else if(arrivalairportcheck.Checked)
                {
                    Query= "select " + cheap + "   F.flight_no, F.arrival_time, F.departure_time, F.flight_price,a.AirportName,a.AirportLoc from flights F,arrives arr, airport a where arr.flight_no=F.flight_no AND arr.AirportName=a.AirportName AND arr.AirportName=" + "\"" + arrivalairport.Text + "\"";
                }
                else if (departureairportcheck.Checked&&arrivalairportcheck.Checked)
                {
                   Query = "select " + cheap + "  F.flight_no, F.arrival_time, F.departure_time, F.flight_price,a1.AirportName,a1.AirportLoc,a2.AirportName,a2.AirportLoc from flights F,departs d, airport a1,airport a2,arrives arr where d.flight_no=F.flight_no AND  arr.flight_no=F.flight_no AND a1.AirportName=" + "\"" + departureairport.Text + "\"" + "AND a2.AirportName=" + "\"" + arrivalairport.Text + "\"";
               }
                else if(departuredatecheck.Checked)
                {
                    DateTime d = departuredate.Value;
                  
                    Query = "select " + cheap + "  D.flight_no, D.departure_time from departs D where date(D.departure_time) =" + "\"" + d.Year + "-" + d.Month + "-" + d.Day + "\"";
                }
                else if (arrivaldatecheck.Checked)
                {
                    DateTime d = arrivaldate.Value;

                    Query = "select " + cheap + "  E.flight_no, E.arrival_time from arrives E,flights F where F.flight_no=E.flight_no and date(E.arrival_time) =" + "\"" + d.Year + "-" + d.Month + "-" + d.Day + "\"";
                }
                else if(departuredatecheck.Checked&&arrivaldatecheck.Checked)
                {
                    DateTime d1 = arrivaldate.Value;
                    DateTime d2 = departuredate.Value;
                    Query = "select " + cheap + "  E.flight_no, E.arrival_time from arrives E, departs D where date(E.arrival_time) =" + "\"" + d1.Year + "-" + d1.Month + "-" + d1.Day + "\" and date(D.departure_time) =" + "\"" + d2.Year + "-" + d2.Month + "-" + d2.Day + "\"";
                }
                else if (departureairportcheck.Checked && cheapestflight.Checked)
                {

                    //  "\"" + value + "\""
                    //cheap = "all";
                    Query = "select all D.flight_no, A.AirportName, F.flight_price from departs D, airport A, flights F where F.flight_no = D.flight_no and " + "\"" + departureairport.Text + "\"  = A.AirportName  order by F.flight_price ASC";
                }


                string Connection = "server=localhost;user id=root;pwd=bartender;database=airline_assistant";
               
                //string Query = "select all  F.flight_no, F.arrival_time, F.departure_time, F.flight_price from flights F";
               
                MySqlConnection Conn = new MySqlConnection(Connection);


                
                MySqlDataAdapter adapter = new MySqlDataAdapter(Query, Conn);
                    
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
               


                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

      

       
    }
}
