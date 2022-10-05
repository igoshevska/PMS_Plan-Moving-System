using System.Collections.Generic;
using System.Linq;
namespace MoveIT.Extensions
{
    public class SignalRUsersInMemory<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock(_connections)
            {
                HashSet<string> connetions;
                if(!_connections.TryGetValue(key, out connetions))
                {
                    connetions = new HashSet<string>();
                    _connections.Add(key, connetions);
                }
                lock(connetions)
                {
                    connetions.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;

            if(_connections.TryGetValue(key, out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)

        {
            lock(_connections)
            {
                HashSet<string> connection;
                if(_connections.TryGetValue(key, out connection))
                {
                    return;
                }
                lock(_connections)
                {
                    connection.Remove(connectionId);
                    
                    if(connection.Count ==0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }

    }
}
