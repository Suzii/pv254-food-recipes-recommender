using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipes.DAL.Helpers;
using System.Text.RegularExpressions;
using EnglishStemmer;

namespace Recipes.TFIDF.TFIDF
{
    /// <summary>
    ///  Rewritten code from
    ///  Copyright (c) 2013 Kory Becker http://www.primaryobjects.com/kory-becker.aspx
    /// </summary>
    public static class TFIDFHelper
    {
        /// <summary>
        /// Transforms a list of documents into their associated TF*IDF values.
        /// </summary>
        /// <param name="documents">IList<RecipeDocumentHelper></param>
        /// <returns>Dictionary<int, List<double>> </returns>
        public static Dictionary<int, List<double>> Transform(IList<RecipeDocumentHelper> documents)
        {
            Dictionary<int,List<string>> stemmedDocs;
            HashSet<string> vocabulary;
            Dictionary<string, double> vocabularyIDF = new Dictionary<string, double>();

            GetVocabulary(documents,out stemmedDocs, out vocabulary);

            // Calculate the IDF for each vocabulary term.
            foreach (var term in vocabulary)
            {
                double numberOfDocsContainingTerm = stemmedDocs.Where(d => d.Value.Contains(term)).Count();
                vocabularyIDF[term] = 1 + Math.Log((double)stemmedDocs.Count / ((double)numberOfDocsContainingTerm));
            }

            return TransformToTFIDFVectors(stemmedDocs, vocabularyIDF);
        }

        /// <summary>
        /// Calculates cosine similarity between two vectors
        /// </summary>
        /// <param name="vector1">vector1</param>
        /// <param name="vector2">vector2</param>
        /// <returns>cosine similarity value</returns>
        public static double CosineSimilarity(List<double> vector1, List<double> vector2)
        {

            int N = 0;
            N = ((vector2.Count < vector1.Count) ? vector2.Count : vector1.Count);
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            for (int n = 0; n < N; n++)
            {
                dot += vector1[n] * vector2[n];
                mag1 += Math.Pow(vector1[n], 2);
                mag2 += Math.Pow(vector2[n], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }

        /// <summary>
        /// Normalizes a TF*IDF array of vectors using L2-Norm.
        /// Xi = Xi / Sqrt(X0^2 + X1^2 + .. + Xn^2)
        /// </summary>
        /// <param name="vectors">Dictionary<int, List<double>></param>
        /// <returns>Dictionary<int, List<double>></returns>
        public static Dictionary<int, List<double>> Normalize(Dictionary<int, List<double>> vectors)
        {
            // Normalize the vectors using L2-Norm.
            Dictionary<int, List<double>> normalizedVectors = new Dictionary<int, List<double>>();
            foreach (var vector in vectors)
            {
                var normalized = Normalize(vector.Value);
                normalizedVectors.Add(vector.Key, normalized);
            }

            return normalizedVectors;
        }

        /// <summary>
        /// Converts a list of stemmed documents (lists of stemmed words) and their associated vocabulary + idf values, into an array of TF*IDF values.
        /// </summary>
        /// <param name="stemmedDocs">Dictionary of List of string</param>
        /// <param name="vocabularyIDF">Dictionary of string, double (term, IDF)</param>
        /// <returns>Dictionary<int, List<double>></returns>
        private static Dictionary<int, List<double>> TransformToTFIDFVectors(Dictionary<int, List<string>> stemmedDocs, Dictionary<string, double> vocabularyIDF)
        {
            // Transform each document into a vector of tfidf values.
            Dictionary<int, List<double>> vectors = new Dictionary<int, List<double>>();
            foreach (var doc in stemmedDocs)
            {
                List<double> vector = new List<double>();

                foreach (var vocab in vocabularyIDF)
                {
                    // Term frequency = count how many times the term appears in this document.
                    double tf = doc.Value.Where(d => d == vocab.Key).Count()/(double) doc.Value.Count;
                    double tfidf = tf * vocab.Value;

                    vector.Add(tfidf);
                }

                vectors.Add(doc.Key, Normalize(vector));
            }

            return vectors;
        }

        /// <summary>
        /// Parses and tokenizes a list of documents, returning a vocabulary of words.
        /// </summary>
        /// <param name="documents"> documets to parse </param>
        /// <param name="stemmedDocs"> list od stemmed documets with their id</param>
        /// <param name="vocabulary"> global vocabulary</param>
        private static void GetVocabulary(IList<RecipeDocumentHelper> documents, out Dictionary<int, List<string>> stemmedDocs, out HashSet<string> vocabulary)
        {
            vocabulary = new HashSet<string>();
            stemmedDocs = new Dictionary<int, List<string>>();

            int docIndex = 0;

            foreach (var doc in documents)
            {
                List<string> stemmedDoc = new List<string>();

                docIndex++;

                string[] parts = Tokenize(doc.Document);

                List<string> words = new List<string>();
                foreach (string part in parts)
                {
                    // Strip non-alphanumeric characters.
                    string stripped = Regex.Replace(part, "[^a-zA-Z0-9]", "");

                    if (!StopWords.stopWordsList.Contains(stripped.ToLower()))
                    {
                        try
                        {
                            var english = new EnglishWord(stripped);
                            string stem = english.Stem;
                            words.Add(stem);

                            if (stem.Length > 0)
                            {                               
                                stemmedDoc.Add(stem);
                                vocabulary.Add(stem);
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                stemmedDocs.Add(doc.RecipeId, stemmedDoc);
            }
        }


        /// <summary>
        /// Tokenizes a string, returning its list of words.
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>string[]</returns>
        private static string[] Tokenize(string text)
        {
            // Strip all HTML.
            text = Regex.Replace(text, "<[^<>]+>", "");

            // Strip numbers.
            text = Regex.Replace(text, @"[\d]+", "number");

            text = Regex.Replace(text, @"number[CF]|[Gg]as number", "temperature");
            // Strip urls.
            // text = Regex.Replace(text, @"(http|https)://[^\s]*", "httpaddr");

            // Strip email addresses.
            //text = Regex.Replace(text, @"[^\s]+@[^\s]+", "emailaddr");

            // Strip dollar sign.
            text = Regex.Replace(text, "[$]+", "dollar");

            // Strip usernames.
            //text = Regex.Replace(text, @"@[^\s]+", "username");

            // Tokenize and also get rid of any punctuation
            return text.Split(" @$/#.-:&*+=[]?!(){},''\">_<;%\\".ToCharArray()).Where(x => !String.IsNullOrEmpty(x)).ToArray();
        }

        /// <summary>
        /// Normalizes a TF*IDF vector using L2-Norm.
        /// Xi = Xi / Sqrt(X0^2 + X1^2 + .. + Xn^2)
        /// </summary>
        /// <param name="vectors">List<double></param>
        /// <returns>List<double></returns>
        private static List<double> Normalize(List<double> vector)
        {
            List<double> result = new List<double>();

            double sumSquared = 0;
            foreach (var value in vector)
            {
                sumSquared += value * value;
            }

            double SqrtSumSquared = Math.Sqrt(sumSquared);

            foreach (var value in vector)
            {
                // L2-norm: Xi = Xi / Sqrt(X0^2 + X1^2 + .. + Xn^2)
                result.Add(value / SqrtSumSquared);
            }

            return result;
        }


    }
}
