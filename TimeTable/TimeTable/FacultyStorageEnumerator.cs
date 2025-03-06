using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    internal class FacultyStorageEnumerator : IEnumerator<Faculty>
    {
        private readonly Faculty[] faculties;
        private int currentIndex = -1;
        private bool disposedValue;

        public FacultyStorageEnumerator(Faculty[] faculties)
        {
            this.faculties = faculties;
        }

        public Faculty Current => faculties[currentIndex];
        object IEnumerator.Current => faculties[currentIndex];
        public bool MoveNext()
        {
            currentIndex++;
            if (currentIndex >= faculties.Length)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }

        // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~FacultyEnumerator()
        // {
        //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}