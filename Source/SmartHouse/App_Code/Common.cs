using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
	public Common()
	{
        m_SessionHouse = "houseid";
        m_SessionUser = "user";
        m_SessionHouseIP = "houseip";
        m_HousePort = 25252;
        m_Break = "###";
        m_SessionPass = "pass";
	}

    string m_SessionUser;
    string m_SessionHouse;
    string m_SessionHouseIP;
    string m_Break;
    string m_SessionPass;
    int m_HousePort;

    public string getSessionPass()
    {
        return m_SessionPass;
    }

    public int getHousePort()
    {
        return m_HousePort;
    }
    public string getSessionUser()
    {
        return m_SessionUser;
    }

    public string getSessionHouse()
    {
        return m_SessionHouse;
    }

    public string getSessionHouseIP()
    {
        return m_SessionHouseIP;
    }
    public string getBreak()
    {
        return m_Break;
    }
}