using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nhom_11_DuongBaVan_11CDTM1
{
    class BFS
    {
        private Queue<int> VertexQueue = new Queue<int>(); // hang doi chua cac dinh
        private List<List<int>> _adjacent; // danh sach canh ke
        private List<int> _reportPath = new List<int>(); // danh sach dinh duong di
        
        public BFS(List<List<int>> adjacent)
        {
            this._adjacent = adjacent;
            
        }
       
        //find path
        public List<int> findPathbyBfs(int tips, int start, int end)
        {
            if (this._adjacent[start] == null || this._adjacent[end] == null)
                return null;

            bool[] visited = new bool[tips]; // danh dau cac dinh da tham
            int[] previous = new int[tips + 1]; // luu dinh truoc
         

            // khoi tao mang visited va previous
            for (int index = 0; index < tips; ++index)
            {
                visited[index] = false;
                previous[index] = -1;
            }

            this.VertexQueue.Enqueue(start);
            visited[start] = true;
            while (this.VertexQueue.Count != 0)
            {
                int v = this.VertexQueue.Dequeue();
                
                List<int> row = new List<int>(this._adjacent[v]);
                if (row != null)
                {
                    foreach (int col in row)
                    {
                        if (!visited[col])
                        {
                            this.VertexQueue.Enqueue(col);
                            previous[col] = v;
                            visited[col] = true;
                            // neu diem ket thu duoc tham thi ket thuc thuat toan
                            if (visited[end]) break;     
                        }
                    }
                }
                // diem cuoi duoc tham thi ket thuc thuat toan
                if (visited[end]) break;
                    
            }
            // neu diem ket thuc khong duoc tham thi tra ve null
            if (!visited[end]) return null;

            // truy vet duong di
            int current = end;
            this._reportPath.Add(end);
            while (previous[current] != -1)
            {
                this._reportPath.Add(previous[current]);
                current = previous[current];
            }
            this._reportPath.Reverse();
            return this._reportPath;
        }

    } // end find path

}
