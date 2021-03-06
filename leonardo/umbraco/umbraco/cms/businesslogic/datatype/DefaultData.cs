using System;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace Umbraco.Cms.BusinessLogic.datatype
{
	public class DefaultData : interfaces.IData
	{
		private int _propertyId;
		private object _value;
		protected BaseDataType _dataType;
		
		public DefaultData(BaseDataType DataType) {
			_dataType = DataType;
		}

        public void Initialize(object InitValue, int InitPropertyId)
        {
            _propertyId = InitPropertyId;
            _value = InitValue;
        }

		#region IData Members

		public virtual System.Xml.XmlNode ToXMl(System.Xml.XmlDocument d)
		{
			if (this._dataType.DBType == DBTypes.Ntext) 
				return d.CreateCDataSection(this.Value.ToString());
			return d.CreateTextNode(Value.ToString());
		}
		
		public object Value 
		{
			get 
			{
				return _value;
			}
			set 
			{
				// Try to set null values if possible
				try 
				{
					if (value == null)
						Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Umbraco.GlobalSettings.DbDSN, CommandType.Text, "update cmsPropertyData set "+ _dataType.DataFieldName +" = NULL where id = " + _propertyId);
					else
						Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Umbraco.GlobalSettings.DbDSN, CommandType.Text, "update cmsPropertyData set "+ _dataType.DataFieldName +" = @value where id = " + _propertyId, new SqlParameter("@value", value) );
					_value = value;
				} 
				catch (Exception e)
				{
					Umbraco.BusinessLogic.Log.Add(Umbraco.BusinessLogic.LogTypes.Debug, Umbraco.BusinessLogic.User.GetUser(0), -1, "Error updating item: " + e.ToString());
					if (value==null) value ="";
					Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Umbraco.GlobalSettings.DbDSN, CommandType.Text, "update cmsPropertyData set "+ _dataType.DataFieldName +" = @value where id = " + _propertyId, new SqlParameter("@value", value) );
					_value = value;
				}

			}
		}

		public virtual void MakeNew(int PropertyId)
		{
			// this default implementation of makenew does not do anything s�nce 
			// it uses the default datastorage of Umbraco, and the row is already populated by the "property" object
			// If the datatype needs to have a default value, inherit this class and override this method.
		}

		public void Delete() {
			// this default implementation of delete does not do anything s�nce 
			// it uses the default datastorage of Umbraco, and the row is already deleted by the "property" object
		}

		public int PropertyId
		{
			get {
				return _propertyId;
			}
			set
			{
				_propertyId = value;
				_value = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Umbraco.GlobalSettings.DbDSN, CommandType.Text,"Select " + _dataType.DataFieldName + " from cmsPropertyData where id = " + value);
			}
		}

		// TODO: clean up Legacy - these are needed by the wysiwyeditor, in order to feed the richtextholder with version and nodeid
		// solution, create a new version of the richtextholder, which does not depend on these.
		public Guid Version {
			get {
				return  new Guid(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Umbraco.GlobalSettings.DbDSN, CommandType.Text,"Select versionId from cmsPropertyData where id = " + PropertyId).ToString());
			}
		}

		public int NodeId {
			get {
				return  int.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Umbraco.GlobalSettings.DbDSN, CommandType.Text,"Select contentNodeid from cmsPropertyData where id = " + PropertyId).ToString());
			}
		}
		#endregion
	}	
}
