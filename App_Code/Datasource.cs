﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace RemaxWebsite
{
    public static class Datasource
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RemaxDatabase;Integrated Security=True";
        public static DataSet dataSet = new DataSet();

        public static AgentsList GetAllAgents()
        {
            AgentsList agentList = new AgentsList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Agent";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(dataSet, "Agent");

                foreach (DataRow row in dataSet.Tables["Agent"].Rows)
                {
                    string AgentID = row["_id"].ToString();
                    string FirstName = row["FirstName"].ToString();
                    string LastName = row["LastName"].ToString();
                    string Email = row["Email"].ToString();
                    string Nip = row["Nip"].ToString();
                    string Phone = row["Phone"].ToString();
                    string Address = row["Address"].ToString();
                    string City = row["City"].ToString();
                    string Image = row["Image"].ToString();

                    Agent agent = new Agent(AgentID, FirstName, LastName, Email, Nip, Phone, Address, City, Image);
                    agentList.Add(agent);
                }
            }

            return agentList;
        }

        public static ClientsList GetAgentClients(string AgentID)
        {
            ClientsList clientsList = new ClientsList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Client WHERE refAgent = '{AgentID}'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(dataSet, "Client");

                foreach (DataRow row in dataSet.Tables["Client"].Rows)
                {
                    string ClientID = row["_id"].ToString();
                    string FirstName = row["FirstName"].ToString();
                    string LastName = row["LastName"].ToString();
                    string Email = row["Email"].ToString();
                    string Nip = row["Nip"].ToString();
                    string Phone = row["Phone"].ToString();

                    Client client = new Client(ClientID, FirstName, LastName, Email, Nip, Phone);
                    clientsList.Add(client);
                }
            }

            return clientsList;
        }

        public static HousesList GetClientHouses(string ClientID)
        {
            HousesList housesList = new HousesList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM House WHERE refClient = '{ClientID}'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(dataSet, "House");

                foreach (DataRow row in dataSet.Tables["House"].Rows)
                {
                    string Number = row["Number"].ToString();
                    string CivicNumber = row["CivicNumber"].ToString();
                    string Type = row["Type"].ToString();
                    string Model = row["Model"].ToString();
                    string Country = row["Country"].ToString();
                    string Province = row["Province"].ToString();
                    string City = row["City"].ToString();
                    string PostalCode = row["PostalCode"].ToString();
                    string Image = row["Image"].ToString();
                    Decimal CurrentPrice = Convert.ToDecimal(row["CurrentPrice"]);

                    House house = new House(Number, CivicNumber, Type, Model, Country, Province, City, PostalCode, Image, CurrentPrice);
                    housesList.Add(house);
                }
            }

            return housesList;
        }

        public static HousesList GetAllHouses()
        {
            HousesList housesList = new HousesList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM House";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(dataSet, "House");

                foreach (DataRow row in dataSet.Tables["House"].Rows)
                {
                    string Number = row["_id"].ToString();
                    string CivicNumber = row["Address"].ToString();
                    string Type = row["Type"].ToString();
                    string Model = row["Model"].ToString();
                    string Country = row["Country"].ToString();
                    string Province = row["Province"].ToString();
                    string City = row["City"].ToString();
                    string PostalCode = row["PostalCode"].ToString();
                    string Image = row["Image"].ToString();
                    Decimal CurrentPrice = Convert.ToDecimal(row["Price"]);

                    House house = new House(Number, CivicNumber, Type, Model, Country, Province, City, PostalCode,Image, CurrentPrice);
                    housesList.Add(house);
                }
            }

            return housesList;
        }

        public static HousesList GetHousesByParam(string col, string filter)
        {
            HousesList housesList = new HousesList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM House WHERE {col} = @filter";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@filter", filter);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "House");

                foreach (DataRow row in dataSet.Tables["House"].Rows)
                {
                    string Number = row["_id"].ToString();
                    string CivicNumber = row["Address"].ToString();
                    string Type = row["Type"].ToString();
                    string Model = row["Model"].ToString();
                    string Country = row["Country"].ToString();
                    string Province = row["Province"].ToString();
                    string City = row["City"].ToString();
                    string PostalCode = row["PostalCode"].ToString();
                    string Image = row["Image"].ToString();
                    Decimal CurrentPrice = Convert.ToDecimal(row["Price"]);

                    House house = new House(Number, CivicNumber, Type, Model, Country, Province, City, PostalCode, Image, CurrentPrice);
                    housesList.Add(house);
                }
            }

            return housesList;
        }

        public static AgentsList GetAgentsByParam(string col, string filter)
        {
            AgentsList agentsList = new AgentsList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Agent WHERE {col} = @filter";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@filter", filter);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Agent");

                foreach (DataRow row in dataSet.Tables["Agent"].Rows)
                {
                    string _id = row["_id"].ToString();
                    string FirstName = row["FirstName"].ToString();
                    string LastName = row["LastName"].ToString();
                    string Email = row["Email"].ToString();
                    string Nip = row["Nip"].ToString();
                    string Phone = row["Phone"].ToString();
                    string Address = row["Address"].ToString();
                    string City = row["City"].ToString();
                    string Image = row["Image"].ToString();

                    Agent agent = new Agent(_id, FirstName, LastName, Email, Nip, Phone, Address, City, Image);
                    agentsList.Add(agent);
                }
            }

            return agentsList;
        }



    }
}
