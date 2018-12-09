using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent8 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/8.txt"; } }

        public override void A()
        {
            CaseName = "8A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            TreeDef = Regex.Matches(input[0], "[\\d]+").ToList();

            var nodes = new List<Node>();

            while(TreeDef.Count > 0)
            {
                nodes.Add(ParseNode());
            }

            var sum = 0;
            foreach(var n in nodes)
            {
                sum += GetMetadataSum(n);
            }

            Console.WriteLine(sum);

            End();
        }

        private List<Match> TreeDef { get; set; }

        public Node ParseNode()
        {
            var node = new Node();
            var childCount = int.Parse(TreeDef[0].Value);
            TreeDef.RemoveAt(0);
            var metadataCount = int.Parse(TreeDef[0].Value);
            TreeDef.RemoveAt(0);

            node.Children = new List<Node>();
            for (int i = 0; i < childCount; i++)
            {
                node.Children.Add(ParseNode());
            }

            node.Metadata = new List<int>();
            for (int i = 0; i < metadataCount; i++)
            {
                node.Metadata.Add(int.Parse(TreeDef[0].Value));
                TreeDef.RemoveAt(0);
            }
            return node;
        }

        private int GetMetadataSum(Node n)
        {
            var result = 0;
            foreach(var c in n.Children)
            {
                result += GetMetadataSum(c);
            }

            result += n.Metadata.Sum();

            return result;
        }

        private int GetMetadataValue(Node n)
        {
            var result = 0;
            if (n.Children.Count == 0)
                return n.Metadata.Sum();

            for(int i = 0; i < n.Metadata.Count; i++)
            {
                if (n.Metadata[i] > 0 && n.Metadata[i] <= n.Children.Count)
                    result += GetMetadataValue(n.Children[n.Metadata[i]-1]);
            }

            return result;
        }

        public override void B()
        {
            CaseName = "8B";
            Start();

            var input = Input.GetInputFromFile(INPUT);

            TreeDef = Regex.Matches(input[0], "[\\d]+").ToList();

            var nodes = new List<Node>();

            while (TreeDef.Count > 0)
            {
                nodes.Add(ParseNode());
            }

            var sum = GetMetadataValue(nodes[0]);

            Console.WriteLine(sum);

            End();

        }

    }

    public class Node
    {
        public char ID { get; set; }
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }

    }
}
