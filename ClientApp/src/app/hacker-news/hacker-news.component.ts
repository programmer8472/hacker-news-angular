import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hacker-news',
  templateUrl: './hacker-news.component.html',
  styleUrls: ['./hacker-news.component.css']
})
export class HackerNewsComponent implements OnInit {
  //public forecasts: WeatherForecast[];
  //public articleIds: number[];
  public articlesOriginal: Article[];
  public articles: Article[];
  public p: number = 1;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.GetAllArticles(http, baseUrl);
  }

  GetAllArticles(http: HttpClient, @Inject('BASE_URL') baseUrl: string): void {
    this.articles = [];
    this.articlesOriginal = [];

    http.get<number[]>(baseUrl + 'HackerNews').subscribe((results) => {

      results.forEach((element) => {

        http.get<Article>(baseUrl + "HackerNews/ArticleDetail?id=" + element).subscribe(detail => {

          this.articles.push(detail);
          this.articlesOriginal.push(detail);

        });

      });

    }, error => console.error(error));
  }

  ngOnInit() {
  }


  public search() {
    this.articles = this.articlesOriginal;
    const searchText = ((document.getElementById("text-search") as HTMLInputElement).value);
    this.articles = this.articles.filter(article => {
      return article.title.includes(searchText);
    });
  }

}

interface Article {
  title: string;
  url: string;
  by: string;
}

