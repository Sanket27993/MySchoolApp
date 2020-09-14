using System;
using System.Collections.Generic;


namespace MySchool
{
    /// <summary>
    /// this class for access database 
    /// </summary>
    public class DataLayer: IEventSendResponse
    {
        public event EventHandler<ResponseCodeEventArgs> EventSendResponse;

        readonly  IDatabaseconnection _databaseconnection;
        public DataLayer()
        {
            _databaseconnection = DatabaseConnection.Instance;
        }

        public void CreateTabel<T>(ICreateTabel createTabel)
        {
            createTabel.Execute<T>(_databaseconnection);
        }
        public void Save(ISave save)
        {
            var result = save.Execute(_databaseconnection);
            if (result ==(int) DbResponse.success)
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Save, Message = "Success" });
            else
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Save, Message = "Fail" });
        }
        public void SaveWithoutReplace(ISave save)
        {
            var result = save.ExecuteWithoutReplace(_databaseconnection);
            if (result == (int)DbResponse.success)
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Save, Message = "Success" });
            else
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Save, Message = "Fail" });
        }
        public void Update(IUpdate update)
        {
            var result = update.Execute(_databaseconnection);
            if (result ==(int) DbResponse.success)
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Update, Message = "Success" });
            else
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Update, Message = "Fail" });
        }
        public void Delete(IDelete delete)
        {
           var result= delete.DeleteExecute(_databaseconnection);
            if (result == (int)DbResponse.success)
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Delete, Message = "Success" });
            else
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Delete, Message = "Fail" });
        }
        public List<T> Fetch<T>(IFetch fetch) where T : new()
        {
            var result = fetch.Execute<T>(_databaseconnection);
            return result;
        }
        public T FetchSingle<T>(IFetch fetch) where T : new()
        {
            var result = fetch.ExecuteSingle<T>(_databaseconnection);
            return result;
        }
        public void ClearTable(IClearTable clearTable, string tablename)
        {
            var result = clearTable.Execute(tablename, _databaseconnection);
            if (result == (int)DbResponse.success)
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Clear, Message = "Success" });
            else
                EventSendResponse(this, new ResponseCodeEventArgs { Result = result, Code = (int)CodeDataLayer.Clear, Message = "Fail" });
        }


    }
    /// <summary>
    /// return DataLayer object 
    /// </summary>
    public static class GNDataLayer
    {
        public static DataLayer GetNew()
        {
            return new DataLayer();
        }

    }
}
