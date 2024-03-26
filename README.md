# Wheatstone cipher
This variation of the Wheatston cipher consists of two words (one word for each matrix), but may consist of one (not fill the keyword).
Wheatstone encryption uses two 5x5 matrices, which are either one below the other in (vertical variation). Each of the matrices is first filled with the corresponding keyword without writing any repeated letters, then the remaining cells of the matrix are filled in order with alphabet characters (without letter Q) that were not previously used. In this way the 5x5 matrix is completely filled.

# Algorithm:
1. Break the message into bigrams (groups of two characters).
2. Find the first digram symbol in the upper matrix and the second one in the lower matrix.
3. Determine the positions of the symbols relative to each other. If the letters of the original message bigram are in the same row, the first letter of the encrypted bigram is taken from the left matrix in the column in which the second letter of the original bigram is located. The second letter of the encrypted bigram is taken from the second matrix in the column with the first letter of the original bigram.

As an example, below is a vertical Wheatstone cipher with the keywords "example" and "keyword":

<table>
  <tr>
    <td>E</td>
    <td>X</td>
    <td>A</td>
    <td>M</td>
    <td>P</td>
  </tr>
  <tr>
    <td>L</td>
    <td>B</td>
    <td>C</td>
    <td>D</td>
    <td>F</td>
  </tr>
  <tr>
    <td>G</td>
    <td>H</td>
    <td>I</td>
    <td>J</td>
    <td>K</td>
  </tr>
  <tr>
    <td>N</td>
    <td>O</td>
    <td>R</td>
    <td>S</td>
    <td>T</td>
  </tr>
  <tr>
    <td>U</td>
    <td>V</td>
    <td>W</td>
    <td>Y</td>
    <td>Z</td>
  </tr>
</table>

<table>
  <tr>
    <td>K</td>
    <td>E</td>
    <td>Y</td>
    <td>W</td>
    <td>O</td>
  </tr>
  <tr>
    <td>R</td>
    <td>D</td>
    <td>A</td>
    <td>B</td>
    <td>C</td>
  </tr>
  <tr>
    <td>F</td>
    <td>G</td>
    <td>H</td>
    <td>I</td>
    <td>J</td>
  </tr>
  <tr>
    <td>L</td>
    <td>M</td>
    <td>N</td>
    <td>P</td>
    <td>S</td>
  </tr>
  <tr>
    <td>T</td>
    <td>U</td>
    <td>V</td>
    <td>X</td>
    <td>Z</td>
  </tr>
</table>

Let's say you need to encrypt the plaintext hello world using the keywords "example" and "keyword". The bigrams of this message will be replaced as follows:
1. The bigram HE has a unique case, it is located in one column, replace it with XG.
2. Bigram LL also has a unique case, it is located in the first column, replace it with NR.
3. The bigram OW forms a rectangle, replace it with SE.
4. The bigram OR forms a rectangle, replace it with ND.
5. The bigram LD forms a rectangle, replace it with BR.
Thus, we get an encrypted message:

Plain text: he ll ow or ld
Ciphertext: XG NR SE ND BR

Please note that the code may not be perfect and exceptions may occur during encryption)
