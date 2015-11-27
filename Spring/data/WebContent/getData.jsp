<%@ page language="java" contentType="text/html; charset=utf-8"
    pageEncoding="utf-8"%>
<%@ page import="data.GetItem" %>  
<% 
String param1 = request.getParameter("busno");
String param2 = request.getParameter("judge");

String url = "http://localhost:56281/getData.aspx";
String result = "";

try
{
	if(param1 != null)
	{
		if(param2 != null)
		{
			if(param2.toLowerCase().equals("yes"))
			{
				url += "?busno="+param1+"&judge=yes";
				System.out.println(url);
				result = new GetItem(url).getJSON();
			}
			else
			{
				url += "?busno="+param1;
				result = new GetItem(url).getJSON();
			}
		}
		else
		{
			url += "?busno="+param1;
			result = new GetItem(url).getJSON();
		}
			
	}
}
catch(Exception e)
{
	result = "Error";
}


%>
<%=result %>