export interface SearchBooksResponse {
    bookId: number,
    title: string,
    author: string,
    description: string,
    publishDate: Date,
    lastModified: Date,
    coverBase64: string
}