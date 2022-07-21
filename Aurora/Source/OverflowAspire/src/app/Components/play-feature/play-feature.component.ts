import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';


interface RecommendedVoices {
  [key: string]: boolean;
}

@Component({
  selector: 'app-play-feature',
  templateUrl: './play-feature.component.html',
  styleUrls: ['./play-feature.component.css']
})

export class PlayFeatureComponent implements OnInit {
  @Input() article: any

  public sayCommand: string;
  public recommendedVoices: RecommendedVoices;
  public rates: number[];
  public selectedRate: number;
  public selectedVoice: SpeechSynthesisVoice | null;
  public text: string;
  public voices: SpeechSynthesisVoice[];

  constructor(private routing: Router, private route: ActivatedRoute) {
    this.voices = [];
    this.rates = [.25, .5, .75, 1, 1.25, 1.5, 1.75, 2];
    this.selectedVoice = null;
    this.selectedRate = 1;
    this.text = this.article;
    this.sayCommand = "";
    this.recommendedVoices = Object.create(null);
    this.recommendedVoices["Alex"] = true;
    this.recommendedVoices["Alva"] = true;
    this.recommendedVoices["Damayanti"] = true;
    this.recommendedVoices["Daniel"] = true;
    this.recommendedVoices["Fiona"] = true;
    this.recommendedVoices["Fred"] = true;
    this.recommendedVoices["Karen"] = true;
    this.recommendedVoices["Mei-Jia"] = true;
    this.recommendedVoices["Melina"] = true;
    this.recommendedVoices["Moira"] = true;
    this.recommendedVoices["Rishi"] = true;
    this.recommendedVoices["Samantha"] = true;
    this.recommendedVoices["Tessa"] = true;
    this.recommendedVoices["Veena"] = true;
    this.recommendedVoices["Victoria"] = true;
    this.recommendedVoices["Yuri"] = true;


  }


  public data: any = new Article();
  IsPause: boolean = false
  ngOnChanges() {
    this.text = this.article
    this.IsPause
  }
  public ngOnInit(): void {

    this.voices = speechSynthesis.getVoices();
    this.selectedVoice = (this.voices[0] || null);
    this.updateSayCommand();
    if (!this.voices.length) {

      speechSynthesis.addEventListener(
        "voiceschanged",
        () => {

          this.voices = speechSynthesis.getVoices();
          this.selectedVoice = (this.voices[0] || null);
          this.updateSayCommand();

        }
      );

    }

  }
  public speak(): void {
    this.IsPause = !this.IsPause
    if (!this.IsPause && speechSynthesis.speaking ) {
      speechSynthesis.pause();
    } if (this.IsPause &&speechSynthesis.speaking ) {

      speechSynthesis.resume();


    }
    if (!this.selectedVoice || !this.text) {

      return;

    }
    if (!speechSynthesis.speaking) {

      this.stop();
      this.synthesizeSpeechFromText(this.selectedVoice, this.selectedRate, this.text);
    }
  }


  public stop(): void {
    this.IsPause = false

    if (speechSynthesis.speaking) {

      speechSynthesis.cancel();

    }

  }
  public pause(): void {

    if (speechSynthesis.speaking) {

      speechSynthesis.pause();

    }

  }

  public resume(): void {

    if (speechSynthesis.speaking) {

      speechSynthesis.resume();

    }

  }



  public updateSayCommand(): void {

    if (!this.selectedVoice || !this.text) {

      return;

    }


    var sanitizedRate = Math.floor(200 * this.selectedRate);
    var sanitizedText = this.text
      .replace(/[\r\n]/g, " ")
      .replace(/(["'\\\\/])/g, "\\$1")
      ;

    this.sayCommand = `say --voice ${this.selectedVoice.name} --rate ${sanitizedRate} --output-file=demo.aiff "${sanitizedText}"`;

  }

  private synthesizeSpeechFromText(
    voice: SpeechSynthesisVoice,
    rate: number,
    text: string
  ): void {

    var utterance = new SpeechSynthesisUtterance(text);
    utterance.voice = this.selectedVoice;
    utterance.rate = rate;

    speechSynthesis.speak(utterance);

  }
  isShow: boolean = false; // hidden by default
  toggleShown() {

    this.isShow = !this.isShow;

  }

}
