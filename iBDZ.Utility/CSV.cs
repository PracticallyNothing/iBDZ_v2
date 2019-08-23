using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iBDZ.Utility
{
	public class CSVLine
	{
		public CSVLine(List<string> data)
		{
			Data = data;
		}

		public CSVLine(List<string> headings, List<string> data)
		{
			Headings = headings;
			Data = data;
		}

		public List<string> Headings { get; }
		public List<string> Data { get; }

		public bool HasHeadings => Headings != null && Headings.Count > 0;
		public int NumColumns => Data.Count;

		public string this[int index]
		{
			get
			{
				if (index >= NumColumns)
				{
					throw new IndexOutOfRangeException("CSVLine: Index larger than number of columns in line.");
				}
				else if (index < 0)
				{
					throw new IndexOutOfRangeException("CSVLine: Index less than 0.");
				}
				else
				{
					return Data[index];
				}
			}
		}

		public string this[string headingName]
		{
			get
			{
				if (!HasHeadings)
					throw new Exception("CSV file has no headings.");
				else if (!Headings.Contains(headingName))
					throw new Exception(String.Format("CSV file doesn't have a heading named '{0}'.", headingName));
				else
					return this[Headings.IndexOf(headingName)];
			}
		}
	}

	public class CSV
	{
		protected CSV(List<string> headings, List<CSVLine> data, string filename)
		{
			HasHeadings = true;
			Headings = headings;
			Data = data;
			NumColumns = headings.Count;
			Filename = filename;
		}

		protected CSV(List<CSVLine> data, string filename)
		{
			HasHeadings = false;
			Data = data;
			NumColumns = data[0].NumColumns;
			Filename = filename;
		}

		public string Filename { get; }
		public bool HasHeadings { get; }
		public List<string> Headings { get; }
		public List<CSVLine> Data { get; }
		public int NumColumns { get; }

		public static CSV ReadFile(string filename, string delimiter, bool hasHeading)
		{
			string[] lines = File.ReadAllLines(filename);

			if (hasHeading)
			{
				List<string> headings = lines[0].Split(delimiter).ToList();
				List<CSVLine> data = new List<CSVLine>();
				foreach (var line in lines.Skip(1))
				{
					CSVLine csvLine = new CSVLine(headings, line.Split(delimiter).Select(x => x.Trim()).ToList());
					data.Add(csvLine);
				}
				return new CSV(headings, data, filename);
			}
			else
			{
				List<CSVLine> data = new List<CSVLine>();
				foreach (var line in lines)
				{
					data.Add(new CSVLine(line.Split(delimiter).Select(x => x.Trim()).ToList()));
				}
				return new CSV(data, filename);
			}
		}
	}
}
