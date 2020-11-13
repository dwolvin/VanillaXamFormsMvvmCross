using System;
namespace vanilla.Core.Models
{
    public class TidePrediction
    {

        public TidePrediction()
        {
            

        }
    }
}

/*
 * request:  
 * https://api.tidesandcurrents.noaa.gov/api/prod/datagetter
 * ?product=predictions
 * &application=NOS.COOPS.TAC.WL
 * &begin_date=20201104
 * &end_date=20201105
 * &datum=MLLW
 * &station=8638596
 * &time_zone=lst_ldt
 * &units=english
 * &interval=hilo
 * &format=json
 * 
 * { "predictions" : [
{"t":"2020-11-04 04:40", "v":"0.378", "type":"L"},{"t":"2020-11-04 11:19", "v":"3.046", "type":"H"},{"t":"2020-11-04 17:37", "v":"0.476", "type":"L"},{"t":"2020-11-04 23:47", "v":"2.434", "type":"H"},{"t":"2020-11-05 05:19", "v":"0.434", "type":"L"},{"t":"2020-11-05 12:00", "v":"2.978", "type":"H"},{"t":"2020-11-05 18:22", "v":"0.518", "type":"L"}
]}*/
