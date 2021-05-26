using BDF.VehicleTracker.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDF.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for StravaActivities.xaml
    /// </summary>
    public partial class StravaActivities : Window
    {
        //Configuration.Default.AccessToken = "bbdf9c6e5f1618f477b5c2e8e4b38d902f322e17";
        
        const string bearerToken = "073acb13886ed8555de22c5b0ff74df9c4639b37";
        List<BL.Models.Activity> activities;
        int activityCount = 10;

        public StravaActivities()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void DecodeMaps()
        {
            //string polygon = "opcuGskv{Aw@q@w@JgJjKkIbFiK`@kEiBkCgF}B_Pq@{ByI_HwLqT{ByF}@gEqA{LaEqLgBeP{HcP}D_LmBoIu@cGUuGpA{OYsC{@yBuDiEmGeLqF}FiTsNoMiE{EoDkC{DmDyHeGgFmA_DeBsJuAuB_SqOcMaDsHsGwGc@iB_BkE}J_@_EwAwFI_C`@iCj@uAxGqGzAoCbDyKnDcSMcJb@}Jn@ac@a@iEgFgPo@{AgBmB{JkAkCqBwEiN_AmKqCuJeDuJcKeVoKk^_DeFaLsH_FaHkFsBqAyAoFeSyByLcByDuGkH}KwGuF{FyGwDaDeEmIcC}BoAyBuCwDoH{CyAsG{@wBqAyDiEmByDmCgHq@cI}@wAi@OuF~LyEzHoAhDoDv_@uB`Nw@|BIxBq@xByEzGiHtGcBfEaBzH{DfG_J`JgN`JsJbPeAj@mCHwBq@yFgEaFJiXbGcCfAgCrCqF`KgEvCkEPoNnCqN_@kIpBk@l@uAhIy@`CkBvBmFxC_@p@{AfJmBfD{AjFIfFe@~AuEdBeD`C}@MuC}BsALy@t@_@dBYrJRvCIjDsEjFaDpBa@fBRdBzBtC@nA}AlDI`A|BfIMrBiAtD~@zGaAtFk@j@{@@}CuAeCDgOaFmCd@uCpCuCr@wCjI{@jFaAbA{EvBc@|@c@pDaFnHObAHfGYtAcDvB_A~D_B~De@dD}BtEg@xDaCtDs@~GGzCm@fCmBnC_DxBoDzIsCxDgBf@qO|Aa@nAU`Gg@xDiA~CiAtAsBtAoCSoA\\{D`EiBdEaDv@aEeBmGi@kDaCcIcIgEu@uCcBeFgAgMl@_BfBo@zBDpJWjBi@r@qA^mJr@uCdB}ApCkAtNwCvGc@fEPhEh@dEhBdG|BvCzF|CcMkA_B_AiBiCaFsUMqLaAkCwAx@m@dCrAlNe@tFk@hA{EnEy@LkAu@mEkK_Em@}Cf@}@`BoBlHoA~GeC|ToB|FPxLuBvI{DpBeFzEkEt@aI`DyAnG{BvDc@xFcCjHcD~@eAbBa@dC{@fBeEfDwEx@m@h@kAvCyDz@wFoAmCiDy@mBs@YmEGwCn@_LsAy@jAsEpQeAbBiAh@eDUkCZy@Y{AmBwAMeJ|JkFfHyDtC_@lAO`Do@~AiF~@{@p@U|@JdAzAfCh@xDG~Ca@~@mCd@iKoAcHpGuFpI[fAk@bN{CnLqAvAiN`I{FyAcD}C{DcGsCiAwIfCyIt@aKzBkKv@iEs@wJuIcEuA}DFmHlCeDf@[ZGhAf@bLcCdMGvIYnBgDpEYfD{PjF";

            activities.ForEach(a => GooglePoints.Decode(a.map.summary_polyline));

            //IEnumerable <CoordinateEntity> points = GooglePoints.Decode(polygon);

            //points.ToList().ForEach(p => lbxPoints.Items.Add(p.Latitude + " : " + p.Longitude));
            //this.Title = "Done";
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.strava.com/api/v3/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            return client;
        }

        private void Reload()
        {

            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                double endDate = DateHelper.ConvertToUnixTimestamp(DateTime.Now);
                double startDate = DateHelper.ConvertToUnixTimestamp(new DateTime(2019, 1, 1));

                if(dpStartDate.SelectedDate.HasValue)
                    startDate = DateHelper.ConvertToUnixTimestamp(dpStartDate.SelectedDate.Value);

                if (dpEndDate.SelectedDate.HasValue)
                    endDate = DateHelper.ConvertToUnixTimestamp(dpEndDate.SelectedDate.Value);

                // Call the API Color Controller/Get
                response = client.GetAsync("athlete/activities?before="+ endDate.ToString() + "&after=" + startDate.ToString() + "&per_page=" + activityCount.ToString()).Result;
                //response = client.GetAsync("activities").Result;
                // Get the JSON response
                result = response.Content.ReadAsStringAsync().Result;
                // Split the JSON response into a JArray.
                items = (JArray)JsonConvert.DeserializeObject(result);
                // Convert the JArray to List<Color>
                activities = items.ToObject<List<BL.Models.Activity>>();

                activities.ForEach(a => a.map.points = GooglePoints.Decode(a.map.summary_polyline));

                lbxNames.ItemsSource = activities;
                lbxNames.DisplayMemberPath = "name";
                lbxNames.SelectedValuePath = "id";

                string msg = activities.Count() + " activities between " + startDate.ToString() + " & " + endDate.ToString();
                lblInfo.Content = msg;

            }
            catch (Exception ex)
            {

                lblInfo.Content = ex.Message;
            }

        }

        private void GetActivity(long objectId)
        {

            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;
                BL.Models.Activity activity;

                response = client.GetAsync("activities/" + objectId).Result;
                result = response.Content.ReadAsStringAsync().Result;
                activity = JsonConvert.DeserializeObject<BL.Models.Activity>(result);
                
                activity.map.points = GooglePoints.Decode(activity.map.summary_polyline);
                activities = new List<Activity>();
                activities.Add(activity);

                lbxNames.ItemsSource = activities;
                lbxNames.DisplayMemberPath = "name";
                lbxNames.SelectedValuePath = "id";


            }
            catch (Exception ex)
            {

                lblInfo.Content = ex.Message;
            }

        }

        private void lbxNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity activity = activities[lbxNames.SelectedIndex];

            lbxPoints.ItemsSource = activity.map.points;
            lbxPoints.DisplayMemberPath = "LatLng";
            lbxPoints.SelectedValuePath = "Id";

            txtId.Text = activity.id.ToString();
            string msg = activity.map.points.Count() + " map points...";
            lblInfo.Content = msg;
            txtActivityDate.Text = activity.start_date_local.ToString();
            txtMiles.Text = (activity.distance / 1610).ToString("N2");
            txtPower.Text = activity.average_watts.ToString();
            txtMovingTime.Text = DateHelper.ConvertToTimeFormat(activity.moving_time);
            txtCalories.Text = activity.kilojoules.ToString();

        }

        private void btnGetActivity_Click(object sender, RoutedEventArgs e)
        {
            GetActivity(5313923325);
        }

        private void btnRefreshToken_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.strava.com/api/v3/oauth/");

            string parameters = "?client_id=38221";
            parameters += "&client_secret=7fd80b410ecda2345355cca1e83e346de49dd484";
            parameters += "&grant_type=refresh_token";
            parameters += "&refresh_token=f0948bd4e962022df22739791d90db19a0d05ee8";

            HttpResponseMessage response;
            //string result;
            //dynamic items;

            // Call the API Color Controller/Get
            response = client.PostAsync("token" + parameters, null).Result;

            string result = response.Content.ReadAsStringAsync().Result;
            // Split the JSON response into a JArray.
            SToken sToken = JsonConvert.DeserializeObject<SToken>(result);
            string msg = "Expires at " + DateHelper.ConvertFromUnixTimestamp(Convert.ToDouble(sToken.expires_at)) + " UTC";
            lblInfo.Content = msg;
        }

        private void slActivityCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            activityCount = Convert.ToInt32(Math.Truncate(slActivityCount.Value));
            lblActivityCount.Content = activityCount.ToString();
        }

    }
}
