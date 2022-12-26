using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Lemmaizers;

namespace TagCloudPainter.Tests;

public class Tests
{
    public Lemmaizer Lemmaizer { get; set; }

    [SetUp]
    public void Setup()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Lemmaizers", "mystem.exe");

        Lemmaizer = new Lemmaizer(path);
    }

    [TestCase(null, TestName = "{m}_null")]
    [TestCase(" ", TestName = "{m}_WhiteSpace")]
    [TestCase("", TestName = "{m}_EmptyString")]
    public void GetLemma_Should_Fail_On(string word)
    {
        Action action = () => Lemmaizer.GetLemma(word);

        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase(null, TestName = "{m}_null")]
    [TestCase(" ", TestName = "{m}_WhiteSpace")]
    [TestCase("", TestName = "{m}_EmptyString")]
    public void GetMorph_Should_Fail_On(string word)
    {
        Action action = () => Lemmaizer.GetMorph(word);

        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase("��������", ExpectedResult = "A", TestName = "{m}_A_On_Adjective")]
    [TestCase("�����", ExpectedResult = "ADV", TestName = "{m}_ADV_On_Adverb")]
    [TestCase("���", ExpectedResult = "ADVPRO", TestName = "{m}_ADVPRO_On_Pronominal_Adverb")]
    [TestCase("��������", ExpectedResult = "ANUM", TestName = "{m}_ANUM_On_Numeral_Adjective")]
    [TestCase("�����", ExpectedResult = "APRO", TestName = "{m}_APRO_On_Pronoun_Adjective")]
    [TestCase("��", ExpectedResult = "CONJ", TestName = "{m}_CONJ_On_Conjunction")]
    [TestCase("��-��-��", ExpectedResult = "INTJ", TestName = "{m}_INTJ_On_interjection")]
    [TestCase("���", ExpectedResult = "NUM", TestName = "{m}_NUM_On_numeral")]
    [TestCase("��", ExpectedResult = "PART", TestName = "{m}_PART_On_PART")]
    [TestCase("�����", ExpectedResult = "PR", TestName = "{m}_PR_On_pretext")]
    [TestCase("����", ExpectedResult = "S", TestName = "{m}_S_On_noun")]
    [TestCase("���-��", ExpectedResult = "SPRO", TestName = "{m}_SPRO_On_pronoun_noun")]
    [TestCase("����", ExpectedResult = "V", TestName = "{m}_V_On_VERB")]
    public string GetMorph_Should_Return(string str)
    {
        var word = Lemmaizer.GetMorph(str);

        return word;
    }

    [TestCase("���", ExpectedResult = "����", TestName = "{m}_Verb")]
    [TestCase("���������", ExpectedResult = "��������", TestName = "{m}_Adjective")]
    [TestCase("��������", ExpectedResult = "�������", TestName = "{m}_Noun")]
    [TestCase("����-��", ExpectedResult = "���-��", TestName = "{m}_pronoun_noun")]
    [TestCase("������", ExpectedResult = "�����", TestName = "{m}_Pronoun_Adjective")]
    public string GetLemma_Should_Return_NormalForm_On(string str)
    {
        var word = Lemmaizer.GetLemma(str);

        return word;
    }
}