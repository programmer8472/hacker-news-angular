import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hacker-news',
  templateUrl: './hacker-news.component.html',
  styleUrls: ['./hacker-news.component.css']
})
export class HackerNewsComponent {
  //public forecasts: WeatherForecast[];
  //public articleIds: number[];
  public articlesAll: Article[];
  public articles: Article[];

  public search() {
    this.articles = this.articlesAll;
    const searchText = ((document.getElementById("text-search") as HTMLInputElement).value);
    this.articles = this.articles.filter(article => {
      return article.title.includes(searchText);
    });
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.articles = [];
    this.articlesAll = [];

    http.get<number[]>('https://hacker-news.firebaseio.com/v0/newstories.json').subscribe((results) => {
      
      results.forEach((element) => {
        //console.log(element);
        http.get<Article>("https://hacker-news.firebaseio.com/v0/item/" + element + ".json").subscribe(detail => {
          
          this.articles.push(detail);
          this.articlesAll.push(detail);

        });
      });

    }, error => console.error(error));

  }

  GetAllArticles(): void {

  }

}

interface Article {
  title: string;
  url: string;
  by: string;
}

