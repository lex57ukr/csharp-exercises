// This file was auto-generated based on version 1.0.0 of the canonical data.

using Xunit;

  public class TwoFerTest
  {
      [Fact]
      public void No_name_given()
      {
          Assert.Equal("One for you, one for me.", TwoFer.GetResponse());
      }

      [Fact]
      public void Name_is_given()
      {
          Assert.Equal("One for Alice, one for me.", TwoFer.GetResponse("Alice"));
      }

      [Fact]
      public void Another_name_is_given()
      {
          Assert.Equal("One for Bob, one for me.", TwoFer.GetResponse("Bob"));
      }
  }
