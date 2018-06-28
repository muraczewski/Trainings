using System.Collections.Generic;
using System.Linq;

namespace Trainings
{
    public class ListWrapper<T> where T : IRequiredFields, new()
    {
        private List<T> _list;

        public ListWrapper()
        {
            _list = new List<T>();
        }

        public ListWrapper(List<T> list)
        {
            _list = list;
        }

        public void Add(T element)
        {
            _list.Add(element);
        }

        public void Remove(T element)
        {
            _list.Remove(element);
        }

        public T Get(int id)
        {
            return _list.FirstOrDefault(l => l.Id == id);
        }

        public void AddEmpty()
        {
            _list.Add(new T());
        }

    }

    public interface IRequiredFields
    {
        int Id { get; }
    }
}