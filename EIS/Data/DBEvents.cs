using Microsoft.Data.SqlClient;
using EIS.Models;
using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;
using System.Globalization;

namespace EIS.Data
{
    //public class DBEvents
    //{
    //    public string _ConnectionStrVC { get; set; }

    //    public DBEvents(string ConnectionStrVC)
    //    {
    //        _ConnectionStrVC = ConnectionStrVC;
    //    }

    //    private SqlConnection GetConnection()
    //    {
    //        SqlConnection conn = new SqlConnection(_ConnectionStrVC);
    //        conn.Open();

    //        return conn;
    //    }

    //    private void CloseConnection(SqlConnection conn)
    //    {
    //        conn.Close();
    //    }

    //    public List<Event> GetCalendarEvents(string start, string end)
    //    {
    //        List<Event> events = new List<Event>();

    //        using (SqlConnection conn = GetConnection())
    //        {
    //            using (SqlCommand cmd = new SqlCommand(@"select
    //                                                        Id
    //                                                        ,Title
    //                                                        ,[Description]
    //                                                        ,EventStart
    //                                                        ,EventEnd
    //                                                        ,AllDay
    //                                                        ,TextColor
    //                                                        ,BackgroundColor
    //                                                        ,BorderColor
    //                                                        ,DisplayOrder
    //                                                    from
    //                                                        [MESEvents]
    //                                                    where
    //                                                        EventStart between @start and @end", conn)
    //            {
    //                CommandType = CommandType.Text
    //            })
    //            {
    //                cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
    //                cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
    //                using (SqlDataReader dr = cmd.ExecuteReader())
    //                {
    //                    while (dr.Read())
    //                    {
    //                        events.Add(new Event()
    //                        {
    //                            EventId = Convert.ToInt32(dr["Id"])+1,
    //                            Title = Convert.ToString(dr["Title"]),
    //                            Description = Convert.ToString(dr["Description"]),
    //                            Start = Convert.ToDateTime(dr["EventStart"]).ToString("O"),
    //                            End = DBNull.Value.Equals(dr["EventEnd"]) ? Convert.ToDateTime(dr["EventStart"]).ToString("O") : Convert.ToDateTime(dr["EventEnd"]).ToString("O"),
    //                            AllDay = Convert.ToBoolean(dr["AllDay"]),
    //                            TextColor = Convert.ToString(dr["TextColor"]),
    //                            BackgroundColor = Convert.ToString(dr["BackgroundColor"]),
    //                            BorderColor = Convert.ToString(dr["BorderColor"]),
    //                            DisplayOrder = Convert.ToString(dr["DisplayOrder"])
    //                        });
    //                        //"2023-04-02T00:00:00+02:00"
    //                    }
    //                }
    //            }
    //        }

    //        return events;
    //    }

    //    public string UpdateEvent(Event evt)
    //    {
    //        string message = "";
    //        SqlConnection conn = GetConnection();
    //        SqlTransaction trans = conn.BeginTransaction();

    //        try
    //        {
    //            SqlCommand cmd = new SqlCommand(@"update
    //                                             [MESEvents]
    //                                            set
    //                                             [Description]=@description
    //                                                ,Title=@title
    //                                             ,EventStart=@start
    //                                             ,EventEnd=@end 
    //                                             ,AllDay=@allDay
    //                                                ,TextColor=@textcolor
    //                                                ,BackgroundColor=@backgroundcolor
    //                                                ,BorderColor=@bordercolor
    //                                                ,DisplayOrder=@displayorder
    //                                            where
    //                                             Id=@eventId", conn, trans)
    //            {
    //                CommandType = CommandType.Text
    //            };
    //            cmd.Parameters.Add("@eventId", SqlDbType.Int).Value = evt.EventId;
    //            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = evt.Title;
    //            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = evt.Description;
    //            cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = DateTime.Parse(evt.Start).ToUniversalTime();
    //            cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = DBHelpers.ToDBNullOrDefault(evt.End.Count() == 0 ? "" : DateTime.Parse(evt.End).ToUniversalTime());
    //            cmd.Parameters.Add("@allDay", SqlDbType.Bit).Value = evt.AllDay;
    //            cmd.Parameters.Add("@textcolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.TextColor);
    //            cmd.Parameters.Add("@backgroundcolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.BackgroundColor);
    //            cmd.Parameters.Add("@bordercolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.BorderColor);
    //            cmd.Parameters.Add("@displayorder", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.DisplayOrder);
    //            cmd.ExecuteNonQuery();

    //            trans.Commit();
    //        }
    //        catch (Exception exp)
    //        {
    //            trans.Rollback();
    //            message = exp.Message;
    //        }
    //        finally
    //        {
    //            CloseConnection(conn);
    //        }

    //        return message;
    //    }

    //    public string AddEvent(Event evt, out int eventId)
    //    {
    //        string message = "";
    //        SqlConnection conn = GetConnection();
    //        SqlTransaction trans = conn.BeginTransaction();
    //        eventId = 0;

    //        try
    //        {
    //            SqlCommand cmd = new SqlCommand(@"insert into [MESEvents]
    //                                            (
    //                                             Title
    //                                             ,LineID
    //                                                ,[Description]
    //                                             ,EventStart
    //                                             ,EventEnd
    //                                             ,AllDay
    //                                                ,TextColor
    //                                                ,BackgroundColor
    //                                                ,BorderColor
    //                                                ,DisplayOrder
    //                                            )
    //                                            values
    //                                            (
    //                                             @title
    //                                                ,@lineid
    //                                             ,@description
    //                                             ,@start
    //                                             ,@end
    //                                             ,@allDay
    //                                                ,@textcolor
    //                                                ,@backgroundcolor
    //                                                ,@bordercolor
    //                                                ,@displayorder
    //                                            );
    //                                            select scope_identity()", conn, trans)
    //            {
    //                CommandType = CommandType.Text
    //            };
    //            cmd.Parameters.Add("@lineid", SqlDbType.VarChar).Value = "GBM Line1";
    //            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = evt.Title;
    //            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = evt.Description;
    //            cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = DateTime.Parse(evt.Start).ToUniversalTime();
    //            cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = DBHelpers.ToDBNullOrDefault(evt.End.Count()==0 ? "" : DateTime.Parse(evt.End).ToUniversalTime());
    //            cmd.Parameters.Add("@allDay", SqlDbType.Bit).Value = evt.AllDay;
    //            cmd.Parameters.Add("@textcolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.TextColor);
    //            cmd.Parameters.Add("@backgroundcolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.BackgroundColor);
    //            cmd.Parameters.Add("@bordercolor", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.BorderColor);
    //            cmd.Parameters.Add("@displayorder", SqlDbType.VarChar).Value = DBHelpers.ToDBNullOrDefault(evt.DisplayOrder);

    //            eventId = Convert.ToInt32(cmd.ExecuteScalar());

    //            trans.Commit();
    //        }
    //        catch (Exception exp)
    //        {
    //            trans.Rollback();
    //            message = exp.Message;
    //        }
    //        finally
    //        {
    //            CloseConnection(conn);
    //        }

    //        return message;
    //    }

    //    public string DeleteEvent(int eventId)
    //    {
    //        string message = "";
    //        SqlConnection conn = GetConnection();
    //        SqlTransaction trans = conn.BeginTransaction();

    //        try
    //        {
    //            SqlCommand cmd = new SqlCommand(@"delete from 
    //                                             [MESEvents]
    //                                            where
    //                                             Id=@eventId", conn, trans)
    //            {
    //                CommandType = CommandType.Text
    //            };
    //            cmd.Parameters.Add("@eventId", SqlDbType.Int).Value = eventId;
    //            cmd.ExecuteNonQuery();

    //            trans.Commit();
    //        }
    //        catch (Exception exp)
    //        {
    //            trans.Rollback();
    //            message = exp.Message;
    //        }
    //        finally
    //        {
    //            CloseConnection(conn);
    //        }

    //        return message;
    //    }
    //}
    public class DBEvents
    {
        public string _ConnectionStrVC { get; set; }

        public DBEvents(string ConnectionStrVC)
        {
            _ConnectionStrVC = ConnectionStrVC;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_ConnectionStrVC);
            conn.Open();

            return conn;
        }

        private void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }

        public List<Event> GetCalendarEvents(string start, string end)
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"select
                                                            event_id
                                                            ,title
                                                            ,[description]
                                                            ,event_start
                                                            ,event_end
                                                            ,all_day
                                                            ,text_color
                                                            ,background_color
                                                            ,border_color
                                                            ,display_order
                                                        from
                                                            [Events]
                                                        where
                                                            event_start between @start and @end", conn)
                {
                    CommandType = CommandType.Text
                })
                {
                    cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
                    cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            events.Add(new Event()
                            {
                                EventId = Convert.ToInt32(dr["event_id"]),
                                Title = Convert.ToString(dr["title"]),
                                Description = Convert.ToString(dr["description"]),
                                Start = Convert.ToDateTime(dr["event_start"]).ToString("O"),
                                End = Convert.ToDateTime(dr["event_end"]).ToString("O"),
                                AllDay = Convert.ToBoolean(dr["all_day"]),
                                TextColor = Convert.ToString(dr["text_color"]),
                                BackgroundColor = Convert.ToString(dr["background_color"]),
                                BorderColor = Convert.ToString(dr["border_color"]),
                                DisplayOrder = Convert.ToString(dr["display_order"])
                            });
                        }
                    }
                }
            }

            return events;
        }

        public string UpdateEvent(Event evt)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"update
	                                                [Events]
                                                set
	                                                [description]=@description
                                                    ,title=@title
	                                                ,event_start=@start
	                                                ,event_end=@end 
	                                                ,all_day=@allDay
                                                where
	                                                event_id=@eventId", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@eventId", SqlDbType.Int).Value = evt.EventId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = evt.Title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = evt.Description;
                cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = evt.Start;
                cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = Helpers.ToDBNullOrDefault(evt.End);
                cmd.Parameters.Add("@allDay", SqlDbType.Bit).Value = evt.AllDay;
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }

        public string AddEvent(Event evt, out int eventId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();
            eventId = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(@"insert into [Events]
                                                (
	                                                title
	                                                ,[description]
	                                                ,event_start
	                                                ,event_end
	                                                ,all_day
                                                )
                                                values
                                                (
	                                                @title
	                                                ,@description
	                                                ,@start
	                                                ,@end
	                                                ,@allDay
                                                );
                                                select scope_identity()", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = evt.Title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = evt.Description;
                cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = evt.Start;
                cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = Helpers.ToDBNullOrDefault(evt.End);
                cmd.Parameters.Add("@allDay", SqlDbType.Bit).Value = evt.AllDay;

                eventId = Convert.ToInt32(cmd.ExecuteScalar());

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }

        public string DeleteEvent(int eventId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"delete from 
	                                                [Events]
                                                where
	                                                event_id=@eventId", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@eventId", SqlDbType.Int).Value = eventId;
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }
    }
}
