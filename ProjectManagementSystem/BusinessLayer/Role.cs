using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace BusinessLayer
{
    public class RoleBusiness
    {
        private string jsonFile = AppDomain.CurrentDomain.BaseDirectory + "/role.json";
        public List<Role> GetRoles(int pageNumber = 0, int pageSize = 0)
        {
            List<Role> roleList = new List<Role>();
            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray roleArrary = (JArray)jObject["Roles"];
                    if (roleArrary != null)
                    {
                        int itemcount = 0;
                        foreach (var item in roleArrary)
                        {
                            Role role = new Role();
                            role.RoleID = Convert.ToInt32(item["RoleID"]);
                            role.RoleName = Convert.ToString(item["RoleName"]);
                            role.Deletable = Convert.ToBoolean(item["Deletable"]);
                            role.Editable = Convert.ToBoolean(item["Editable"]);
                            role.Viewable = Convert.ToBoolean(item["Viewable"]);
                            itemcount++;
                            roleList.Add(role);
                        }
                        roleList.ForEach(x => x.RecordCount = itemcount);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            roleList = GetPage(roleList, pageNumber, pageSize);
            return roleList;
        }
        public List<Role> GetPage(List<Role> list, int page = 0, int pageSize = 0)
        {
            if (page > 0 && pageSize > 0)
                return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            else
                return list;
        }
        public bool AddRole(Role role)
        {
            int count = GetRoles().Count + 1;
            string newRole = "{ 'RoleID': " + count + ", 'RoleName':'" + role.RoleName
                + "', 'Viewable': " + Convert.ToString(role.Viewable).ToLower() + ", 'Deletable': "
                + Convert.ToString(role.Deletable).ToLower()
                + ", 'Editable': " + Convert.ToString(role.Editable).ToLower() + "}";

            try
            {
                var json = File.ReadAllText(jsonFile);
                JObject jsonObj = JObject.Parse(json);
                var newRoleItem = JObject.Parse(newRole);
                var roleArrary = jsonObj.GetValue("Roles") as JArray;
                roleArrary.Add(newRoleItem);

                jsonObj["Roles"] = roleArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, newJsonResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
                return false;
            }
            return true;
        }
        public bool UpdateRoles(Role role)
        {
            string json = File.ReadAllText(jsonFile);

            try
            {
                var jObject = JObject.Parse(json);
                JArray roleArrary = (JArray)jObject["Roles"];

                if (roleArrary != null)
                {

                    foreach (var company in roleArrary.Where(obj => obj["RoleID"].Value<int>() == role.RoleID))
                    {
                        company["Deletable"] = role.Deletable;
                        company["Editable"] = role.Editable;
                        company["Viewable"] = role.Viewable;

                    }

                    jObject["Roles"] = roleArrary;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
                return false;
            }
            return true;
        }
    }
}
